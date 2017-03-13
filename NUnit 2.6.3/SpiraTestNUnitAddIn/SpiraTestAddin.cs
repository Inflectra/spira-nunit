using System;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn
{
	/// <summary>
	/// This class is the main entry point for the add-in. It registers the add-in
	/// with the NUnit core module and instantiates the other classes
	/// </summary>
	[NUnitAddin(Type=ExtensionType.Core, Name="SpiraTestAddin", Description="Allows NUnit automated tests to report their execution results back to SpiraTest")]
	public class SpiraTestAddin : IAddin
	{
		internal const string SOURCE_NAME = "SpiraTestNUnitAddIn";
		private const string CLASS_NAME = "SpiraTestAddin::";

		/// <summary>
		/// Installs the add-in extension points
		/// </summary>
		/// <param name="host">The host that contains the extension points</param>
		/// <returns>True or false depending on whether successful or not</returns>
		public bool Install(IExtensionHost host)
		{
			const string METHOD_NAME = "Install: ";

			try
			{
				//Instantiate the suite builder extension point
				IExtensionPoint builders = host.GetExtensionPoint ("SuiteBuilders");
				if (builders == null)
				{
					return false;
				}

				//Install the SpiraTest configuration suite extension
				builders.Install (new SpiraTestConfigurationBuilder());

				//Instantiate the test decorator extension point
				IExtensionPoint decorators = host.GetExtensionPoint ("TestDecorators");
				if (decorators == null)
				{
					return false;
				}

				//Install the SpiraTest Test Case decorator
				decorators.Install (new SpiraTestCaseDecorator());

				//Indicate success
				return true;
			}
			catch (Exception exception)
			{
				//Log error then rethrow
				System.Diagnostics.EventLog.WriteEntry (SOURCE_NAME, CLASS_NAME + METHOD_NAME + exception.Message, System.Diagnostics.EventLogEntryType.Error);			
				throw exception;
			}
		}
	}
}
