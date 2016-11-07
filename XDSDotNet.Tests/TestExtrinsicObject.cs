using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDSDotNet;
using Xunit;

namespace XDSDotNet.Tests
{
    public class TestExtrinsicObject
    {
        [Fact]
        public void ExistingSlot()
        {
            var element = GetItems().ElementAt(10);
            Assert.Equal("20161021141618", element.GetSlotValue("creationTime"));
        }

        [Fact]
        public void SlotNotFound()
        {
            var element = GetItems().ElementAt(10);
            Assert.Null(element.GetSlotValue("foobar"));
        }

        [Fact]
        public void SlotExists()
        {
            var element = GetItems().ElementAt(10);
            Assert.True(element.SlotExists("creationTime"));
            Assert.False(element.SlotExists("foobar"));
        }

        [Fact]
        public void GetClassification()
        {
            var element = GetItems().ElementAt(10);
            var classification = element.GetClassification("urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a");
            Assert.NotNull(classification);
        }

        [Fact]
        public void GetMissingClassification()
        {
            var element = GetItems().ElementAt(10);
            Assert.Null(element.GetClassification("foobar"));
        }

        IEnumerable<ExtrincicObject> GetItems()
        {
            return from element in XElement.Load("query_response_2.xml").Descendants(XMLTagAndAttributeNames.EXTRINSICOBJECT) select new ExtrincicObject(element);
        }

    }




}
