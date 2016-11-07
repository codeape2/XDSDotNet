using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XDSDotNet;
using System.Xml.Linq;

namespace XDSDotNet.Tests
{
    public class TestSlotContainer
    {
        private XElement element;

        public TestSlotContainer()
        {
            element = new XElement("foobar", Requests.CreateSlot("authorPerson", "Hello World"), Requests.CreateSlot("authorInstitution", new[] { "foo", "bar" }));
        }
        [Fact]
        public void HandlesSingleValue()
        {
            var sc = new SlotContainer(element);
            Assert.Equal("Hello World", sc.GetSlotValue("authorPerson"));
        }

        [Fact]
        public void ErrorIfMultipleValues()
        {
            var sc = new SlotContainer(element);
            Assert.Throws<InvalidOperationException>(() => { sc.GetSlotValue("authorInstitution"); });
        }

        [Fact]
        public void HandleMultipleValues()
        {
            var sc = new SlotContainer(element);
            Assert.Equal(new[] { "foo", "bar" }, sc.GetMultipleSlotValues("authorInstitution"));
        }
    }
}
