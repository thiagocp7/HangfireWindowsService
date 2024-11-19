using AppWsHangfire.ServiceWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppWsHangfire
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {

#if DEBUG
            // Executado em modulo RELEASE
            WsHangFire wsHangFire = new WsHangFire();
            wsHangFire.OnStartTeste();
            Thread.Sleep(TimeSpan.FromDays(10));
#else
            // Executado em modulo DEBUG
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WsHangFire()
            };
            ServiceBase.Run(ServicesToRun);
#endif


        }
    }
}
