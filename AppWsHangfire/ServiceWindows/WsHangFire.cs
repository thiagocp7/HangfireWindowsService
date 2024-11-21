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
using Microsoft.Owin.Hosting;

namespace AppWsHangfire.ServiceWindows
{
    partial class WsHangFire : ServiceBase
    {
        private IDisposable _webApp;
        private BackgroundJobServer _server;

        public WsHangFire()
        {
            InitializeComponent();

            // GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireConnection");

            GlobalConfiguration.Configuration
                               .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                               .UseSimpleAssemblyNameTypeSerializer()
                               .UseRecommendedSerializerSettings()
                               .UseSqlServerStorage(@"Data Source=BR-NOT-DEV-19-B\SQLEXPRESS;Initial Catalog=bdHangfire;User ID=hangfire;Password=654321;Encrypt=False");

        }

        protected override void OnStart(string[] args)
        {

            string baseAddress = "http://localhost:5000/"; // Porta e endereço configuráveis
            _webApp = WebApp.Start<Startup>(baseAddress);


            BackgroundJob.Enqueue(() => testeErro()); 

            // TODO: Adicione aqui o código para iniciar seu serviço.
            _server = new BackgroundJobServer();
           
        }

        protected override void OnStop()
        {
            // TODO: Adicione aqui o código para realizar qualquer desmontagem necessária para interromper seu serviço.
            _server.Dispose(); // Para o servidor Hangfire
            _webApp?.Dispose(); // Para o servidor web (SelfHost)
            EventLog.WriteEntry("OWIN parado.", EventLogEntryType.Information);
        }

        public static void testeErro() 
        {
            throw new Exception("Teste de erro!");
            System.IO.File.WriteAllText("C:\\Dev\\testeHangfire.txt", "Rodor o hangfire");
        }

        public void OnStartTeste() 
        {
            OnStart(null);
        }

    }
}
