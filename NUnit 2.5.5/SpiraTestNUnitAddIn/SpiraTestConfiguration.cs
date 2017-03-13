using System;
using System.Reflection;

using NUnit.Core;
using NUnit.Core.Extensibility;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn
{
	/// <summary>
	/// This class manages the connection to the SpiraTest web services API
	/// </summary>
	public class SpiraTestConfiguration : NUnitTestFixture
	{
		private const string CLASS_NAME = "SpiraTestConfiguration::";

		/// <summary>
		/// Constructor method
		/// </summary>
		/// <param name="url">The URL to the SpiraTest instance</param>
		/// <param name="fixtureType">The type of the fixture</param>
		public SpiraTestConfiguration (Type fixtureType) : base (fixtureType)
		{
			//Delegates to base
		}

		/// <summary>
		/// Sets up the connection to the SpiraTest web service for use in the various tests
		/// </summary>
		/// <param name="suiteResult">The test result</param>
		protected override void DoOneTimeSetUp (TestResult suiteResult)
		{
			const string METHOD_NAME = "DoOneTimeSetUp: ";

			try
			{
				//Call the base class
				base.DoOneTimeSetUp (suiteResult);
			}
			catch (Exception exception)
			{
				//Log error then rethrow
				System.Diagnostics.EventLog.WriteEntry (SpiraTestAddin.SOURCE_NAME, CLASS_NAME + METHOD_NAME + exception.Message, System.Diagnostics.EventLogEntryType.Error);
				throw exception;
			}
		}

		/// <summary>
		/// Closes the connection to the SpiraTest web service
		/// </summary>
		/// <param name="suiteResult">The test result</param>
		protected override void DoOneTimeTearDown (TestResult suiteResult)
		{
			const string METHOD_NAME = "DoOneTimeTearDown: ";

			try
			{
				//Call the base class
				base.DoOneTimeTearDown (suiteResult);
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
