using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace XDSDotNet
{
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IProvideAndRegisterDocumentSetITI41
    {
        [OperationContract(Action = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b", ReplyAction = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-bResponse")]
        Message ProvideAndRegisterDocumentSet(Message request);
    }
}