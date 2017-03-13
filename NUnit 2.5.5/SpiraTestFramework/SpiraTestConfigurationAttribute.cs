using System;

namespace Inflectra.SpiraTest.AddOns.SpiraTestNUnitAddIn.SpiraTestFramework
{
	/// <summary>
	/// This stores the configuration information needed to access the SpiraTest server
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public sealed class SpiraTestConfigurationAttribute : Attribute
	{
		private string url;
		private string login;
		private string password;
		private int projectId;
		private Nullable<int> releaseId;
		private RunnerName runner;

		#region Enumerations
		
		public enum RunnerName
		{
			NUnit = 1,
			Selenium = 2
		}

		#endregion

		#region Properties
		
		/// <summary>
		/// The URL to the SpiraTest instance 
		/// </summary>
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		/// <summary>
		/// A valid SpiraTest login
		/// </summary>
		public string Login
		{
			get
			{
				return this.login;
			}
			set
			{
				this.login = value;
			}
		}

		/// <summary>
		/// A valid SpiraTest password
		/// </summary>
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		/// <summary>
		/// The name of the test runner that should be reported back to SpiraTest
		/// </summary>
		public RunnerName Runner
		{
			get
			{
				return this.runner;
			}
			set
			{
				this.runner = value;
			}
		}

		/// <summary>
		/// The SpiraTest project being tested
		/// </summary>
		public int ProjectId
		{
			get
			{
				return this.projectId;
			}
			set
			{
				this.projectId = value;
			}
		}

		/// <summary>
        /// The SpiraTest release/iteration that the results are to be recorded against
		/// </summary>
        public Nullable<int> ReleaseId
		{
			get
			{
				return this.releaseId;
			}
			set
			{
				this.releaseId = value;
			}
		}

        /// <summary>
        /// The SpiraTest Test Set that the results are to be recorded against
        /// </summary>
        public Nullable<int> TestSetId
        {
            get;
            set;
        }

		#endregion

		/// <summary>
		/// The constructor is called when the attribute is set.
		/// </summary>
		/// <param name="url">The URL to the SpiraTest instance</param>
		/// <param name="login">A valid SpiraTest login</param>
		/// <param name="password">A valid SpiraTest password</param>
		/// <param name="projectId">The SpiraTest project being tested</param>
		public SpiraTestConfigurationAttribute (string url, string login, string password, int projectId) 
		{
			//Update the local member variables
			this.url = url;
			this.login = login;
			this.password = password;
			this.projectId = projectId;
			this.releaseId = null;
            this.TestSetId = null;
			this.runner = RunnerName.NUnit;
		}

		/// <summary>
		/// The constructor is called when the attribute is set.
		/// </summary>
		/// <param name="url">The URL to the SpiraTest instance</param>
		/// <param name="login">A valid SpiraTest login</param>
		/// <param name="password">A valid SpiraTest password</param>
		/// <param name="projectId">The SpiraTest project being tested</param>
		/// <param name="runner">The name of the runner that should be reported back to SpiraTest</param>
		public SpiraTestConfigurationAttribute (string url, string login, string password, int projectId, RunnerName runner) 
		{
			//Update the local member variables
			this.url = url;
			this.login = login;
			this.password = password;
			this.projectId = projectId;
			this.releaseId = null;
            this.TestSetId = null;
            this.runner = runner;
		}

		/// <summary>
		/// The constructor is called when the attribute is set.
		/// </summary>
		/// <param name="url">The URL to the SpiraTest instance</param>
		/// <param name="login">A valid SpiraTest login</param>
		/// <param name="password">A valid SpiraTest password</param>
		/// <param name="projectId">The SpiraTest project being tested</param>
		/// <param name="releaseId">The ID of the release being tested</param>
		public SpiraTestConfigurationAttribute (string url, string login, string password, int projectId, int releaseId) 
		{
			//Update the local member variables
			this.url = url;
			this.login = login;
			this.password = password;
			this.projectId = projectId;
			this.releaseId = releaseId;
            this.TestSetId = null;
            this.runner = RunnerName.NUnit;
		}

		/// <summary>
		/// The constructor is called when the attribute is set.
		/// </summary>
		/// <param name="url">The URL to the SpiraTest instance</param>
		/// <param name="login">A valid SpiraTest login</param>
		/// <param name="password">A valid SpiraTest password</param>
		/// <param name="projectId">The SpiraTest project being tested</param>
		/// <param name="releaseId">The ID of the release being tested</param>
		/// <param name="runner">The name of the runner that should be reported back to SpiraTest</param>
		public SpiraTestConfigurationAttribute (string url, string login, string password, int projectId, int releaseId, RunnerName runner) 
		{
			//Update the local member variables
			this.url = url;
			this.login = login;
			this.password = password;
			this.projectId = projectId;
			this.releaseId = releaseId;
            this.TestSetId = null;
            this.runner = RunnerName.NUnit;
			this.runner = runner;
		}

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="url">The URL to the SpiraTest instance</param>
        /// <param name="login">A valid SpiraTest login</param>
        /// <param name="password">A valid SpiraTest password</param>
        /// <param name="projectId">The SpiraTest project being tested</param>
        /// <param name="releaseId">The ID of the release being tested (pass -1 to use the release associated with the test set)</param>
        /// <param name="testSetId">The ID of the test set being tested (pass -1 to not record against a test set)</param>
        /// <param name="runner">The name of the runner that should be reported back to SpiraTest</param>
        public SpiraTestConfigurationAttribute(string url, string login, string password, int projectId, int releaseId, int testSetId, RunnerName runner)
        {
            //Update the local member variables
            this.url = url;
            this.login = login;
            this.password = password;
            this.projectId = projectId;
            if (releaseId == -1)
            {
                this.releaseId = null;
            }
            else
            {
                this.releaseId = releaseId;
            }
            if (testSetId == -1)
            {
                this.TestSetId = null;
            }
            else
            {
                this.TestSetId = testSetId;
            }
            this.runner = RunnerName.NUnit;
            this.runner = runner;
        }
	}
}
