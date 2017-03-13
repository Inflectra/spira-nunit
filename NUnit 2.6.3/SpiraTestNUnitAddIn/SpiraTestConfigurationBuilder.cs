using System;
using System.Reflection;

using NUnit.Core;
using NUnit.Core.Builders;
using NUnit.Core.Extensibility;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn
{
	/// <summary>
	/// SpiraTestConfigurationBuilder knows how to build a SpiraTestConfiguration class
	/// </summary>
    public class SpiraTestConfigurationBuilder : NUnitTestFixtureBuilder, ISuiteBuilder
	{
		private const string CLASS_NAME = "SpiraTestConfigurationBuilder::";

		protected SpiraImportExport.ImportExport spiraTestExecuteProxy;

		#region Properties

		/// <summary>
		/// The current SpiraTest web services proxy class
		/// </summary>
        public SpiraImportExport.ImportExport SpiraTestExecuteProxy
		{
			get
			{
				return this.spiraTestExecuteProxy;
			}
			set
			{
				this.spiraTestExecuteProxy = value;
			}
		}

		#endregion

		/// <summary>
		/// Responsible for creating a new instance of the suite configuration class
		/// </summary>
		/// <param name="type">The type of object to build from</param>
		/// <returns>The created SpiraTestConfiguration object</returns>
		/// <remarks>
		/// This builder delegates all the work to the constructor of the  
		/// extension suite
		/// </remarks>
		protected new TestSuite BuildFrom(Type type)
		{
            const string METHOD_NAME = "BuildFrom: ";

			try
			{
				//Determines if it can build from the passed in type
				if (CanBuildFrom(type))
				{
					return new SpiraTestConfiguration (type);
				}
				else
				{
					return null;
				}
			}
			catch (Exception exception)
			{
				//Log error then rethrow
				System.Diagnostics.EventLog.WriteEntry (SpiraTestAddin.SOURCE_NAME, CLASS_NAME + METHOD_NAME + exception.Message, System.Diagnostics.EventLogEntryType.Error);
				throw exception;
			}
		}
		
		/// <summary>
		/// The builder recognizes the types that it can use by the presence of SpiraTestConfigurationAttribute
		/// </summary>
		/// <param name="type">The type of the attribute</param>
		/// <returns>True if it can be built, false otherwise</returns>
		/// <remarks>
		/// Note that an attribute does not have to be used. You can use any arbitrary
		/// set of rules that can be implemented using reflection on the type.
		/// </remarks>
		public new bool CanBuildFrom(Type type)
		{
			return Reflect.HasAttribute (type, "Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework.SpiraTestConfigurationAttribute", false);
		}
	}
}
