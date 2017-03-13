using System;
using NUnit.Framework;
using Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SampleTestSuite
{
	/// <summary>
	/// Sample test fixture that tests the NUnit SpiraTest integration
	/// </summary>
	[
	TestFixture,
	SpiraTestConfiguration("http://localhost/SpiraTest", "fredbloggs", "PleaseChange", 1, 1, 2, SpiraTestConfigurationAttribute.RunnerName.NUnit)
	]
	public class SampleTestFixture
	{
		protected static int testFixtureState = 1;

		[TestFixtureSetUp]
		public void FixureInit()
		{
			//Set the state to 2
			testFixtureState = 2;
		}

		[SetUp]
		public void Init()
		{
			//Do Nothing
		}

		/// <summary>
		/// Sample test that asserts a failure
		/// </summary>
		[
		Test,
		SpiraTestCase (5)
		]
		public void _01_SampleFailure()
		{
			//Verify the state
			Assert.AreEqual (2, testFixtureState, "*Real Error*: State not persisted");

			//Failure Assertion
			Assert.AreEqual (1, 0, "Failed as Expected");
		}	

		/// <summary>
		/// Sample test that succeeds
		/// </summary>
		[
		Test,
		SpiraTestCase (6)
		]
		public void _02_SamplePass()
		{
			//Verify the state
			Assert.AreEqual (2, testFixtureState, "*Real Error*: State not persisted");

			//Successful assertion
			Assert.AreEqual (1, 1, "Passed as Expected");
		}	

		/// <summary>
		/// Sample test that does not log to SpiraTest
		/// </summary>
		[
		Test
		]
		public void _03_SampleIgnore()
		{
			//Verify the state
			Assert.AreEqual (2, testFixtureState, "*Real Error*: State not persisted");

			//Failure Assertion
			Assert.AreEqual (1, 0, "Failed as Expected");
		}	
	}
}
