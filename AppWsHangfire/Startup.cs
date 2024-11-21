using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;

namespace AppWsHangfire
{
   
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configurar o painel do Hangfire
            app.UseHangfireDashboard("/hangfire");

            app.Run(context =>
            {
                // Retorna uma resposta simples
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Servidor OWIN rodando no Serviço Windows!");
            });
        }
    }
}
