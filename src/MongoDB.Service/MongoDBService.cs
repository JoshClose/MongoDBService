using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MongoDB.Service
{
	public partial class MongoDB : ServiceBase
	{
		public MongoDB()
		{
			InitializeComponent();
		}

		protected override void OnStart( string[] args )
		{
			var startInfo = new ProcessStartInfo
			{
				Arguments = ConfigurationManager.AppSettings["args"],
				CreateNoWindow = true,
				FileName = ConfigurationManager.AppSettings["path"],
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
			};

			try
			{
				Process.Start( startInfo );
			}
			catch( Exception ex )
			{
				EventLog.WriteEntry( "Error starting process: " + ex, EventLogEntryType.Error );
				throw;
			}

			base.OnStart( args );
		}

		protected override void OnStop()
		{
			const string name = "mongod";

			var processes = Process.GetProcessesByName( name );

			if( processes.Length == 0 )
			{
				EventLog.WriteEntry( string.Format( "No '{0}' processes were found.", name ), EventLogEntryType.Warning );
			}

			foreach( var process in processes )
			{
				try
				{
					process.Kill();
					process.Dispose();
				}
				catch( Exception ex )
				{
					EventLog.WriteEntry( "Error stopping process: " + ex, EventLogEntryType.Error );
				}
			}

			base.OnStop();
		}
	}
}
