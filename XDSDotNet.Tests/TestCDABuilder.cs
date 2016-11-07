using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using Xunit;
using Xunit.Abstractions;
using XDSDotNet;
using static XDSDotNet.XMLNamespaces;

namespace XDSDotNet.Tests
{
    public class TestCDABuilder
    {
        private ITestOutputHelper output;
        public TestCDABuilder(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CanBuildCDAWithContent()
        {
            var cdabuilder = new CDABuilder("application/pdf", new byte[] { 1, 2, 3, 4 });
            var element = cdabuilder.CreateCDA();
            Assert.NotNull(element);
            Assert.IsAssignableFrom<XElement>(element);

            Assert.Equal(hl7 + "ClinicalDocument", element.Name);

            var component = element.Elements(hl7 + "component").SingleOrDefault();
            Assert.NotNull(component);

            var nonXMLBody = component.Elements(hl7 + "nonXMLBody").SingleOrDefault();
            Assert.NotNull(nonXMLBody);

            var text = nonXMLBody.Elements(hl7 + "text").SingleOrDefault();
            Assert.NotNull(text);

            Assert.Equal("application/pdf", text.Attribute("mediaType").Value);
            Assert.Equal("B64", text.Attribute("representation").Value);

            Assert.Equal(new byte[] { 1, 2, 3, 4 }, Convert.FromBase64String(text.Value));
        }

        [Fact]
        public void CDACanHaveMetadata()
        {
            var cdabuilder = new CDABuilder("application/pdf", new byte[] { 1, 2, 3, 4 });
            cdabuilder.Title = "Dette er en test";
            cdabuilder.EffectiveTime = new DateTime(2016, 1, 1, 12, 0, 0);

            var element = cdabuilder.CreateCDA();

            AssertValidCDA(element);
        }

        public void AssertValidCDA(XElement xelmt) => AssertValidCDA(new XDocument(xelmt));

        public void AssertValidCDA(XDocument xelmt)
        {
            var errors = new List<ValidationEventArgs>();
            xelmt.Validate(loadSchemas(), (o, e) =>
            {
                errors.Add(e);
                output.WriteLine($"{e.Message} {e.Exception.Message} - {e.Exception.Source} - {e.Exception.LineNumber}");
            });
            Assert.Equal(0, errors.Count);
        }

        private XmlSchemaSet loadSchemas()
        {
            var retval = new XmlSchemaSet();
            retval.Add("urn:hl7-org:v3", "schemas/datatypes-base.xsd");
            retval.Add("urn:hl7-org:v3", "schemas/datatypes.xsd");
            retval.Add("urn:hl7-org:v3", "schemas/voc.xsd");
            retval.Add("urn:hl7-org:v3", "schemas/CDA.xsd");
            retval.Add("urn:hl7-org:v3", "schemas/POCD_MT000040.xsd");            
            retval.Add("urn:hl7-org:v3", "schemas/NarrativeBlock.xsd");
            return retval;
        }
    }
}
