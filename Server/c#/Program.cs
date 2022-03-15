using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibraryServerRESTful
{
    class Program
    {
        public static string baseAddress = "";
        public static int port = 80;
        static void Main(string[] args)
        {
            WebServiceHost hostWeb = new WebServiceHost(typeof(AudioLibraryServerRESTful.Service));
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServiceEndpoint ep = hostWeb.AddServiceEndpoint(typeof(AudioLibraryServerRESTful.IService), new WebHttpBinding(), ip.ToString());
                    ep.EndpointBehaviors.Add(new WebHttpBehavior { HelpEnabled = true });
                    ServiceDebugBehavior stp = hostWeb.Description.Behaviors.Find<ServiceDebugBehavior>();
                    stp.HttpHelpPageEnabled = true;

                    Console.WriteLine("Service Host started @" + ip.ToString());
                }
            }
            hostWeb.Open();

            baseAddress = hostWeb.BaseAddresses[0].AbsoluteUri;
            port = hostWeb.BaseAddresses[0].Port;

            Console.WriteLine("LISTA COMANDI + URI DISPONIBILI:");
            Console.WriteLine("(GET)   \t" + hostWeb.BaseAddresses[0] + "help");
            var methods = typeof(AudioLibraryServerRESTful.IService).GetMethods();
            IEnumerable<string> actions = methods.Where(
                m => m.GetCustomAttributes(typeof(WebInvokeAttribute), true).Count() > 0)
                .Select(m =>
                    ("(" +
                    ((WebInvokeAttribute)m.GetCustomAttributes(typeof(WebInvokeAttribute), true).First()).Method +
                    ")").PadRight(8) + "\t" +
                    hostWeb.BaseAddresses[0] + "" +
                    ((WebInvokeAttribute)m.GetCustomAttributes(typeof(WebInvokeAttribute), true).First()).UriTemplate.Substring(1)
                    );
            Console.WriteLine(string.Join("\r\n", actions.ToArray()));

            Console.Read();
        }
    }
}
