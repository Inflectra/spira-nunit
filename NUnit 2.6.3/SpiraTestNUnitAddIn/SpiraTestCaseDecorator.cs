using System;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn
{
	/// <summary>
	/// SpiraTestCaseDecorator knows how to build a SpiraTestCase class
	/// </summary>
	public class SpiraTestCaseDecorator : ITestDecorator
	{
		private const string CLASS_NAME = "SpiraTestCaseDecorator::";

		/// <summary>
		/// Instantiates the extended Test Case class and passes it the SpiraTest Test Case ID
		/// </summary>
		/// <param name="test">The NUnit Test</param>
		/// <param name="member">The method whose attribute we need to read</param>
		/// <returns></returns>
		public Test Decorate (Test test, System.Reflection.MemberInfo member)
		{
			const string METHOD_NAME = "Decorate: ";

			try
			{
				if (test is TestMethod)
				{
					Attribute attribute = Reflect.GetAttribute (member, "Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework.SpiraTestCaseAttribute", false);
					if (attribute != null)
					{
						//Get the test case id from the test case attribute
						int testCaseId = (int)Reflect.GetPropertyValue (attribute, "TestCaseId", BindingFlags.Public | BindingFlags.Instance);
                        test = new SpiraTestCase((TestMethod)test, testCaseId);
					}
				}

				return test;
			}
			catch (Exception exception)
			{
				//Log error then rethrow
				System.Diagnostics.EventLog.WriteEntry (SpiraTestAddin.SOURCE_NAME, CLASS_NAME + METHOD_NAME + exception.Message, System.Diagnostics.EventLogEntryType.Error);			
				throw exception;
			}
		}
	}
}
