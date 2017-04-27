using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDSDotNet;
using Xunit;

namespace XDSDotNet.Tests
{
    public class TestRequestBuilder
    {
        [Fact]
        public void SingleDocumentWithoutHomeCommunityId()
        {
            var xelement = Requests.GetSingleDocument_ITI43("ruid", "duid");
            Assert.Null(get_hcidElement(xelement));

        }

        [Fact]
        public void SingleDocumentWithHomeCommunityId()
        {
            var xelement = Requests.GetSingleDocument_ITI43("ruid", "duid", "hcid");
            Assert.Equal("hcid", get_hcidElement(xelement).Value);
        }

        private XElement get_hcidElement(XElement xelement)
        {
            return xelement.Descendants(XMLNamespaces.xds + "HomeCommunityId").SingleOrDefault();            
        }
    }
}
