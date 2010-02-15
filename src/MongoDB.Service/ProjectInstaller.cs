using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace MongoDB.Service
{
	[RunInstaller( true )]
	public partial class ProjectInstaller : Installer
	{
		public ProjectInstaller()
		{
			InitializeComponent();
			Committed += ProjectInstallerCommitted;
			BeforeUninstall += new InstallEventHandler( ProjectInstallerBeforeUninstall );
		}

		static void ProjectInstallerCommitted( object sender, InstallEventArgs e )
		{
			try
			{
				var sc = new ServiceController( "MongoDB" );
				sc.Start();
			}
			catch( Exception ex )
			{
				MessageBox.Show( "Could not start MongoDB Service: " + ex );
			}
		}

		static void ProjectInstallerBeforeUninstall( object sender, InstallEventArgs e )
		{
			try
			{
				var sc = new ServiceController( "MongoDB" );
				sc.Stop();
			}
			catch( Exception ex )
			{
				MessageBox.Show( "Could not stop MongoDB Service: " + ex );
			}
		}
	}
}
