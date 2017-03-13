using System;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Web.Services;
using System.Web.Services.Protocols;

using NUnit.Core;
using NUnit.Core.Extensibility;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn
{
	/// <summary>
	/// This class extends the built-in NUnit test case to provide support for logging the execution
	/// results with the configured instance of SpiraTest
	/// </summary>
	public class SpiraTestCase : TestMethod
	{
		private const string CLASS_NAME = "SpiraTestCase::";
        protected const string TEST_EXECUTE_WEB_SERVICES_URL = "/Services/v2_2/ImportExport.asmx";

        protected TestMethod testMethod;
		protected int testCaseId = 0;

		/// <summary>
		/// The constructor method for the class. Sets the local member variables with the passed in values
		/// </summary>
        /// <param name="testMethod">The NUnit Test Method</param>
		/// <param name="testCaseId">The ID of the matching SpiraTest test case</param>
        public SpiraTestCase(TestMethod testMethod, int testCaseId)
            : base(testMethod.Method)
		{
			//Set the member variables
            this.testMethod = testMethod;
			this.testCaseId = testCaseId;
		}

        /// <summary>
        /// Executes the NUnit test case
        /// </summary>
        /// <returns>The test result</returns>
        public override TestResult RunTest()
        {
            const string METHOD_NAME = "RunTest: ";

            try
            {
                //Call the base method to run the result within NUnit
                TestResult result = base.RunTest();

                //Get the URL, Login, Password and ProjectId from the parent test fixture attribute
                Type type = testMethod.FixtureType;
                Attribute attribute = Reflect.GetAttribute(type, "Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework.SpiraTestConfigurationAttribute", false);
                if (attribute == null)
                {
                    throw new Exception("Cannot retrieve the SpiraTestConfiguration attribute from the test fixture");
                }

                //Get the various properties from the attribute
                string url = (string)Reflect.GetPropertyValue(attribute, "Url", BindingFlags.Public | BindingFlags.Instance);
                string login = (string)Reflect.GetPropertyValue(attribute, "Login", BindingFlags.Public | BindingFlags.Instance);
                string password = (string)Reflect.GetPropertyValue(attribute, "Password", BindingFlags.Public | BindingFlags.Instance);
                int projectId = (int)Reflect.GetPropertyValue(attribute, "ProjectId", BindingFlags.Public | BindingFlags.Instance);
                Nullable<int> releaseId = null;
                if (Reflect.GetPropertyValue(attribute, "ReleaseId", BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    releaseId = (Nullable<int>)Reflect.GetPropertyValue(attribute, "ReleaseId", BindingFlags.Public | BindingFlags.Instance);
                }
                Nullable<int> testSetId = null;
                if (Reflect.GetPropertyValue(attribute, "TestSetId", BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    testSetId = (Nullable<int>)Reflect.GetPropertyValue(attribute, "TestSetId", BindingFlags.Public | BindingFlags.Instance);
                }
                string runnerName = Reflect.GetPropertyValue(attribute, "Runner", BindingFlags.Public | BindingFlags.Instance).ToString();

                //Now we need to extract the result information
                int executionStatusId = -1;
                if (!result.Executed)
                {
                    //Set status to 'Not Run'
                    executionStatusId = 3;
                }
                else
                {
                    //If no codes are found, default to blocked;
                    executionStatusId = 5;
                    if (result.IsFailure)
                    {
                        //Set status to 'Failed'
                        executionStatusId = 1;
                    }
                    if (result.IsSuccess)
                    {
                        //Set status to 'Passed'
                        executionStatusId = 2;
                    }
                    if (result.IsError)
                    {
                        //Set status to 'Failed'
                        executionStatusId = 1;
                    }
                }

                //Extract the other information
                string testCaseName = result.Name;
                string message = result.Message;
                string stackTrace = result.StackTrace;
                int assertCount = result.AssertCount;
                DateTime startDate = DateTime.Now.AddSeconds(-result.Time);
                DateTime endDate = DateTime.Now;

                //Instantiate the web-service proxy class and set the URL from the text box
                bool success = false;
                SpiraImportExport.ImportExport spiraTestExecuteProxy = new SpiraImportExport.ImportExport();
                spiraTestExecuteProxy.Url = url + TEST_EXECUTE_WEB_SERVICES_URL;

                //Create a new cookie container to hold the session handle
                CookieContainer cookieContainer = new CookieContainer();
                spiraTestExecuteProxy.CookieContainer = cookieContainer;

                //Attempt to authenticate the user
                success = spiraTestExecuteProxy.Connection_Authenticate(login, password);
                if (!success)
                {
                    throw new Exception("Cannot authenticate with SpiraTest, check the URL, login and password");
                }

                //Now connect to the specified project
                success = spiraTestExecuteProxy.Connection_ConnectToProject(projectId);
                if (!success)
                {
                    throw new Exception("Cannot connect to the specified project, check permissions of user!");
                }

                //Now actually record the test run itself
                SpiraImportExport.RemoteTestRun remoteTestRun = new SpiraImportExport.RemoteTestRun();
                remoteTestRun.TestCaseId = testCaseId;
                remoteTestRun.ReleaseId = releaseId;
                remoteTestRun.TestSetId = testSetId;
                remoteTestRun.StartDate = startDate;
                remoteTestRun.EndDate = endDate;
                remoteTestRun.ExecutionStatusId = executionStatusId;
                remoteTestRun.RunnerName = runnerName;
                remoteTestRun.RunnerTestName = testCaseName;
                remoteTestRun.RunnerAssertCount = assertCount;
                remoteTestRun.RunnerMessage = message;
                remoteTestRun.RunnerStackTrace = stackTrace;
                spiraTestExecuteProxy.TestRun_RecordAutomated1(remoteTestRun);

                //Close the SpiraTest connection
                spiraTestExecuteProxy.Connection_Disconnect();

                //Return the result
                return result;
            }
            catch (Exception exception)
            {
                //Log error then rethrow (catch any errors associated with the diagnostics)
                try
                {
                    System.Diagnostics.EventLog.WriteEntry(SpiraTestAddin.SOURCE_NAME, CLASS_NAME + METHOD_NAME + exception.Message, System.Diagnostics.EventLogEntryType.Error);
                }
                catch {}
                throw exception;
            }
        }
	}
}
