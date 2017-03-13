using System;
using NUnit.Framework;
using Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework;
using Selenium;

namespace SeleniumSampleTest
{
	/// <summary>
	/// Sample test fixture that tests the NUnit SpiraTest integration with the
	/// Selenium-RC .NET Driver
	/// </summary>
	[
	TestFixture,
	SpiraTestConfiguration("http://localhost/SpiraTest", "fredbloggs", "fredbloggs", 1, 1, SpiraTestConfigurationAttribute.RunnerName.Selenium)
	]
	public class GoogleTest
	{
		private static ISelenium selenium;

		[SetUp]
		public void SetupTest()
		{
			//Instantiate the selenium .NET proxy
			selenium = new DefaultSelenium("localhost", 4444, "*iexplore", "http://www.google.com");
			selenium.Start();
		}

		[TearDown]
		public void TeardownTest()
		{
			selenium.Stop();
		}

		/// <summary>
		/// Sample test that searches on Google, passes correctly
		/// </summary>
		[
		Test,
		SpiraTestCase (5)
		]
		public void GoogleSearch()
		{
			//Opens up Google
			selenium.Open("http://www.google.com/webhp");

			//Verifies that the title matches
			Assert.AreEqual("Google", selenium.GetTitle());
			selenium.Type("q", "Selenium OpenQA");

			//Verifies that it can find the Selenium website
			Assert.AreEqual("Selenium OpenQA", selenium.GetValue("q"));
			selenium.Click("btnG");
			selenium.WaitForPageToLoad("5000");
			Assert.IsTrue(selenium.IsTextPresent("www.openqa.org"));
			Assert.AreEqual("Selenium OpenQA - Google Search", selenium.GetTitle());
		}	
	}
}
