using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;

namespace AppWsHangfire.ServiceWindows
{
    partial class WsHangFire : ServiceBase
    {
        private BackgroundJobServer _server;

        public WsHangFire()
        {
            InitializeComponent();

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireConnection");
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Adicione aqui o código para iniciar seu serviço.
            _server = new BackgroundJobServer();
            _server.Start();
        }

        protected override void OnStop()
        {
            // TODO: Adicione aqui o código para realizar qualquer desmontagem necessária para interromper seu serviço.
            _server.Dispose();
        }

        public void OnStartTeste() 
        {
            OnStart(null);
        }

    }
}
