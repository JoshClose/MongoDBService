using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Linq;

namespace MongoDB.Service
{
	[RunInstaller( true )]
	public partial class ProjectInstaller : Installer
	{
		private const string mongoServiceName = "MongoDB";

		public ProjectInstaller()
		{
			InitializeComponent();
			Committed += ProjectInstallerCommitted;
			BeforeUninstall += ProjectInstallerBeforeUninstall;
		}

		private void ProjectInstallerCommitted( object sender, InstallEventArgs e )
		{
			StartService();
			UpdateBatchFile();
			CreateShortcut();
		}

		private void ProjectInstallerBeforeUninstall( object sender, InstallEventArgs e )
		{
			StopService();
			RemoveShortcut();
		}

		private void StartService()
		{
			try
			{
				var sc = new ServiceController( mongoServiceName );
				sc.Start();
			}
			catch( Exception ex )
			{
				MessageBox.Show( "Could not start MongoDB Service: " + ex );
			}
		}

		private void StopService()
		{
			try
			{
				var sc = new ServiceController( mongoServiceName );
				sc.Stop();
			}
			catch( Exception ex )
			{
				MessageBox.Show( "Could not stop MongoDB Service: " + ex );
			}
		}

		private void UpdateBatchFile()
		{
			var targetdir = Context.Parameters["TARGETDIR"];

			using( var stream = File.OpenWrite( Path.Combine( targetdir, "MongoDB Command Prompt.bat" ) ) )
			using( var writer = new StreamWriter( stream ) )
			{
				writer.WriteLine( "@echo MongoDB Command Prompt" );
				writer.WriteLine( "@SET PATH={0};%PATH%", Path.Combine( targetdir, "MongoDb\\bin" ) );
			}
		}

		private void CreateShortcut()
		{
			var targetdir = Context.Parameters["TARGETDIR"];
			var programMenuFolder = Context.Parameters["ProgramMenuFolder"];

			var shell = new IWshRuntimeLibrary.WshShellClass();

			var shortcutPath = Path.Combine( programMenuFolder, "MongoDB\\MongoDB Command Prompt.lnk" );
			var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut( shortcutPath );

			shortcut.TargetPath = "%COMSPEC%";

			shortcut.Arguments = string.Format( "/K \"{0}\"", Path.Combine( targetdir, "MongoDB Command Prompt.bat" ) );

			shortcut.WorkingDirectory = Path.Combine( targetdir, "MongoDB\\bin" );

			shortcut.Description = "Open MongoDB Command Prompt";
			shortcut.Save();
		}

		private void RemoveShortcut()
		{
			var programMenuFolder = Context.Parameters["ProgramMenuFolder"];
			var shortcutPath = Path.Combine( programMenuFolder, "MongoDB\\MongoDB Command Prompt.lnk" );
			File.Delete( shortcutPath );
		}
	}
}
