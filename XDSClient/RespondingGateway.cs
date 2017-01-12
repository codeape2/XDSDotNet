using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDSDotNet;

using static System.Console;

namespace XDSClient
{
    static public class RespondingGateway
    {
        static public void RunUntilKeyboardInput()
        {
            var uri = "http://localhost:9000/RespondingGateway/";
            using (var serviceHost = new ServiceHost(typeof(RespondingGatewayService), new Uri(uri)))
            {
                SetDebugBehavior(serviceHost);
                var binding = CreateBinding();
                serviceHost.AddServiceEndpoint(typeof(ICrossGatewayQueryITI38), binding, "");
                serviceHost.AddServiceEndpoint(typeof(ICrossGatewayRetrieveITI39), binding, "");
                serviceHost.Open();
                WriteLine($"Service running on {uri}");
                WriteLine("Press ENTER to exit");
                ReadLine();
                serviceHost.Close();
            }
        }

        static public Binding CreateBinding()
        {
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            return binding;
        }

        static public void SetDebugBehavior(ServiceHost serviceHost)
        {
            var debug = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
            {
                serviceHost.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }
        }
    }


    public class RespondingGatewayService : ICrossGatewayQueryITI38, ICrossGatewayRetrieveITI39
    {
        public static readonly MessageVersion MESSAGEVERSION = MessageVersion.Soap12WSAddressing10;

        public Message CrossGatewayQuery(Message request)
        {
            Log("Received CrossGatewayQuery request");
            Log(request);

            try
            {
                return Message.CreateMessage(MESSAGEVERSION, "urn:ihe:iti:2007:CrossGatewayQueryResponse", XElement.Load("CrossGatewayQueryResponse.xml"));
            }
            catch (Exception e)
            {
                Log(e);
                throw;
            }
            
        }

        public Message CrossGatewayRetrieve(Message request)
        {
            Log("Received CrossGatewayRetrieve request");
            Log(request);

            try
            {
                return Message.CreateMessage(MESSAGEVERSION, "urn:ihe:iti:2007:CrossGatewayRetrieveResponse", XElement.Load("CrossGatewayRetrieveResponse.xml"));
            }
            catch (Exception e)
            {
                Log(e);
                throw;
            }            
        }

        static public void Log(string str)
        {
            WriteLine($"{DateTime.Now} {str}");
        }

        static public void Log(object o) => Log(o.ToString());
    }
}
