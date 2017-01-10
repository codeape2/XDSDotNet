using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace XDSDotNet
{
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface ICrossGatewayRetrieveITI39
    {
        [OperationContract(Action = "urn:ihe:iti:2007:CrossGatewayRetrieve", ReplyAction = "urn:ihe:iti:2007:CrossGatewayRetrieveResponse")]
        Message CrossGatewayQuery(Message request);
    }
}
