using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XDSDotNet;
using System.Xml.Linq;

namespace XDSDotNet.Tests
{
    public class TestRegistryResponse
    {
        [Fact]
        public void ExtrincicObjectProperties()
        {
            var items = GetItems();
            Assert.Equal(4, items.Count());

            var example = items.ElementAt(0);
            Assert.NotNull(example.Element);

            Assert.Equal("1654144130.6780495280754736976", example.GetExternalIdentifier(XDSStrings.IDENTIFICATIONSCHEME_DOCENTRY_UNIQUEID));
            Assert.Equal("07067448574^^^&1.3.6.1.4.1.2205.2154.3.1.22&ISO", example.GetExternalIdentifier(XDSStrings.IDENTIFICATIONSCHEME_DOCENTRY_PATIENTID));
        }

        [Fact]
        public void PatientAndDocIds()
        {
            var example = GetItems().ElementAt(0);
            Assert.Equal("1654144130.6780495280754736976", example.UniqueId);
            Assert.Equal("07067448574^^^&1.3.6.1.4.1.2205.2154.3.1.22&ISO", example.PatientId);
        }

        [Fact]
        public void GetClassificationSchemes()
        {
            var example = GetItems().ElementAt(0);
            var classificationSchemes = example.GetClassificationSchemes();
            Assert.Equal(7, classificationSchemes.Length);
            Assert.True(classificationSchemes.All(s => XDSStrings.KnownClassificationSchemes.Contains(s)));

            Assert.True(example.HasKnownClassificationSchemesOnly);
        }

        [Fact]
        public void NameAndDescription()
        {
            var example = GetItems().ElementAt(0);
            Assert.Equal("Document 01", example.Name);

            Assert.Equal("comment2", example.Description);
        }

        IEnumerable<ExtrincicObject> GetItems()
        {
            return from element in XElement.Load("registry_query_response.xml").Descendants(XMLTagAndAttributeNames.EXTRINSICOBJECT) select new ExtrincicObject(element);
        }
    }
}
