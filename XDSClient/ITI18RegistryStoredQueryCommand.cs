using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDSDotNet;

using static System.Console;

namespace XDSClient
{
    public class ITI18RegistryStoredQueryCommand
    {
        private const string ENDPOINTNAME = "ITI18";
        private static readonly MessageVersion MESSAGEVERSION = MessageVersion.Soap12WSAddressing10;

        static public int ExecuteFromFile(string requestFilename) => ExecuteUsingRequestBody(XElement.Load(requestFilename));

        static public int ExecuteUsingPatientId(string patientId) => ExecuteUsingRequestBody(Requests.FindDocuments_ITI18(patientId));

        static public int ExecuteUsingRequestBody(XElement requestBody)
        {
            var f = new ChannelFactory<IRegistryStoredQueryITI18>(ENDPOINTNAME);
            var channel = f.CreateChannel();
            var request = Message.CreateMessage(
                MESSAGEVERSION,
                "urn:ihe:iti:2007:RegistryStoredQuery",
                requestBody
            );
            var response = channel.RegistryStoredQuery(request);
            WriteLine(response);
            return 0;
        }
    }
}
