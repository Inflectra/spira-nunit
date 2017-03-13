using System;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework
{
	/// <summary>
	/// This stores the SpiraTest Test Case Id for the specific NUnit test
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
	public sealed class SpiraTestCaseAttribute : Attribute
	{
		private int testCaseId;

		#region Properties

		/// <summary>
		/// The SpiraTest Test Case Id
		/// </summary>
		public int TestCaseId
		{
			get
			{
				return this.testCaseId;
			}
			set
			{
				this.testCaseId = value;
			}
		}

		#endregion

		/// <summary>
		/// The constructor is called when the attribute is set.
		/// </summary>
		/// <param name="testCaseId">The SpiraTest Test Case Id</param>
		public SpiraTestCaseAttribute(int testCaseId)
		{
			//Update the local member variables
			this.testCaseId = testCaseId;
		}
	}
}
