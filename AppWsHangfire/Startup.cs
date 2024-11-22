using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace AppWsHangfire
{
   
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // Configurar Serilog
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console() // Log no console
               .WriteTo.File("logsTesteSerilog.txt", rollingInterval: RollingInterval.Day) // Log em arquivos diários
               .CreateLogger();

            // Configurar o middleware para registrar requisições e respostas
            app.Use(async (context, next) =>
            {
                Log.Information("Requisição recebida: {Method} {Path}", context.Request.Method, context.Request.Path);

                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro não tratado durante a execução da requisição: {Path}", context.Request.Path);
                    throw;
                }

                Log.Information("Resposta enviada: {StatusCode}", context.Response.StatusCode);
            });


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
