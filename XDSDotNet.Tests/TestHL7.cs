using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XDSDotNet;

namespace XDSDotNet.Tests
{
    public class TestHL7
    {
        [Fact]
        public void TestSerialize()
        {
            var xcn = new XCN { Identifier = "a", LastName = "Brenna", FirstName = "Bernt" };
            Assert.Equal("a^Brenna^Bernt", xcn.Serialize());
        }

        [Fact]
        public void SerializationHandlesNullValues()
        {
            var xcn = new XCN { Identifier = "a", FirstName = "Bernt" };
            Assert.Equal("a^^Bernt", xcn.Serialize());
        }

        [Fact]
        public void Deserialize()
        {
            var xcn = HL7Object.Parse<XCN>("a^Brenna^Bernt");
            Assert.NotNull(xcn);
            Assert.IsAssignableFrom<XCN>(xcn);

            Assert.Equal("a", xcn.Identifier);
            Assert.Equal("Brenna", xcn.LastName);
            Assert.Equal("Bernt", xcn.FirstName);
        }

        [Fact]
        public void DeserializeWithEmptyValue()
        {
            var xcn = HL7Object.Parse<XCN>("a^^Bernt");
            Assert.Equal("a", xcn.Identifier);
            Assert.Null(xcn.LastName);
            Assert.Equal("Bernt", xcn.FirstName);
        }

        [Fact]
        public void DeserializeWithValuesMissingAtEnd()
        {
            var xcn = HL7Object.Parse<XCN>("a^Brenna");
            Assert.Equal("a", xcn.Identifier);
            Assert.Equal("Brenna", xcn.LastName);
            Assert.Null(xcn.FirstName);

        }
    }
}
