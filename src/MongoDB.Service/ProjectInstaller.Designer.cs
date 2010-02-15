namespace MongoDB.Service
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.MongoDBProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.MongoDBServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// MongoDBProcessInstaller
			// 
			this.MongoDBProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.MongoDBProcessInstaller.Password = null;
			this.MongoDBProcessInstaller.Username = null;
			// 
			// MongoDBServiceInstaller
			// 
			this.MongoDBServiceInstaller.ServiceName = "MongoDB";
			this.MongoDBServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange( new System.Configuration.Install.Installer[] {
            this.MongoDBProcessInstaller,
            this.MongoDBServiceInstaller} );

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller MongoDBProcessInstaller;
		private System.ServiceProcess.ServiceInstaller MongoDBServiceInstaller;
	}
}