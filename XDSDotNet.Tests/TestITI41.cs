using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XDSDotNet;
using System.IO;
using static XDSDotNet.XMLNamespaces;

namespace XDSDotNet.Tests
{
    public class TestITI41
    {
        [Fact]
        public void TestGeneratedXml()
        {
            var pards = new ProvideAndRegisterDocumentSet_ITI41("pid", "sid", "Et dokument", "text/plain", new byte[] { 1, 2, 3 });
            pards.AddSubmissionSetClassification(new XDSClassification { ClassificationScheme = "CS", CodingScheme = "CoS", NodeRepresentation = "NR", Name = "N" });
            pards.AddDocumentClassification(new XDSClassification { ClassificationScheme = "CS2", CodingScheme = "CoS2", NodeRepresentation = "NR2", Name = "N2" });
            var request = pards.CreateRequestBody();

            var externalIdentifiers = request.Descendants(XMLNamespaces.rim + "ExternalIdentifier");
            Assert.Equal(5, externalIdentifiers.Count());

            var classifications = request.Descendants(XMLNamespaces.rim + "Classification");
            Assert.Equal(2 + 1, classifications.Count());
        }

        [Fact]
        public void TestGeneratedXmlEquals()
        {
            var pards = new ProvideAndRegisterDocumentSet_ITI41("pid", "sid", "Et dokument", "text/plain", new byte[] { 1, 2, 3 });
            pards.AddSubmissionSetClassification(new XDSClassification { ClassificationScheme = "CS", CodingScheme = "CoS", NodeRepresentation = "NR", Name = "N" });
            pards.AddDocumentClassification(new XDSClassification { ClassificationScheme = "CS2", CodingScheme = "CoS2", NodeRepresentation = "NR2", Name = "N2" });
            pards.AddDocumentSlot(new XDSSlot { Name = "SName", Value = "SValue" });

            pards.DocumentId = "docid";
            pards.SubmissionSetId = "ssid";
            pards.SubmissionTime = new DateTime(2016, 1, 1);
            var request = pards.CreateRequestBody();

            Assert.Equal(File.ReadAllText("iti41_baseline.xml"), request.ToString());
        }

        [Fact]
        public void RequestWithAuthor()
        {
            var pards = new ProvideAndRegisterDocumentSet_ITI41("pid", "sid", "Et dokument", "text/plain", new byte[] { 1, 2, 3 });
            pards.AuthorPerson = "AUTHORPERSON";
            pards.AuthorInstitutions = new[] { "I1", "I2" };

            var request = pards.CreateRequestBody();
            var classification = request.Descendants(rim + "Classification").Where(e => e.Attribute("classificationScheme")?.Value == XDSStrings.CLASSIFICATIONSCHEME_AUTHOR).SingleOrDefault();
            Assert.NotNull(classification);
            Assert.Equal("author", classification.Attribute("nodeRepresentation").Value);
            var slots = classification.Descendants(rim + "Slot");
            Assert.Equal(2, slots.Count());

        }
    }
}
