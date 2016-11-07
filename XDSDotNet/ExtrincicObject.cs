using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;


using static XDSDotNet.XMLNamespaces;
using static XDSDotNet.XMLTagAndAttributeNames;


namespace XDSDotNet
{
    public class SlotContainer
    {
        private XElement element;

        public XElement Element => element;

        public SlotContainer(XElement element)
        {
            this.element = element;
        }

        public string GetSlotValue(string slotName)
        {
            var slot = (from s in Element.Descendants(rim + "Slot") where s.Attribute("name").Value == slotName select s).SingleOrDefault();

            return slot != null ? slot.Elements(rim + "ValueList").Single().Elements(rim + "Value").Single().Value : null;
        }

        public IEnumerable<string> GetMultipleSlotValues(string slotName)
        {
            var slot = (from s in Element.Descendants(rim + "Slot") where s.Attribute("name").Value == slotName select s).SingleOrDefault();
            if (slot == null)
            {
                return Enumerable.Empty<string>();
            }

            return slot.Elements(rim + "ValueList").Single().Elements(rim + "Value").Select(e => e.Value);
        }
    }

    public class ExtrincicObject
    {
        private XElement element;

        public XElement Element => element;


        public ExtrincicObject(XElement element)
        {
            Debug.Assert(element.Name == EXTRINSICOBJECT);
            this.element = element;            
        }

        public string GetSlotValue(string slotName)
        {
            var slot = (from s in Element.Descendants(rim + "Slot") where s.Attribute("name").Value == slotName select s).SingleOrDefault();
            
            return slot != null ? slot.Elements(rim + "ValueList").Single().Elements(rim + "Value").Single().Value : null;
        }

        public string GetExternalIdentifier(string identificationScheme)
        {
            return element.Elements(EXTERNALIDENTIFIER).Single(e => e.Attribute("identificationScheme").Value == identificationScheme).Attribute("value").Value;
        }

        public bool SlotExists(string slotName)
        {
            var slot = (from s in Element.Descendants(rim + "Slot") where s.Attribute("name").Value == slotName select s).SingleOrDefault();
            return slot != null;
        }

        public bool ClassificationExists(string classificationScheme)
        {
            return false;
        }

        public XElement GetClassification(string classificationScheme)
        {
            return element.Elements(CLASSIFICATION).SingleOrDefault(e => e.Attribute("classificationScheme")?.Value == classificationScheme);
        }

        public string[] GetClassificationSchemes()
        {
            var retval = (from e in element.Elements(CLASSIFICATION) select e.Attribute("classificationScheme").Value).ToArray();
            Debug.Assert(retval.Distinct().Count() == retval.Length);
            return retval;
        }
        public bool HasKnownClassificationSchemesOnly
        {
            get
            {
                return GetClassificationSchemes().All(s => XDSStrings.KnownClassificationSchemes.Contains(s));
            }
        }

        public bool HasUnknownClassificationSchemes
        {
            get
            {
                return !HasKnownClassificationSchemesOnly;
            }
        }

        public string Name
        {
            get
            {
                return element.Element(rim + "Name")?.Element(rim + "LocalizedString")?.Attribute("value")?.Value;
            }
        }

        public string Description
        {
            get
            {
                return element.Element(rim + "Description").Element(rim + "LocalizedString").Attribute("value").Value;
            }
        }

        public string UniqueId => GetExternalIdentifier(XDSStrings.IDENTIFICATIONSCHEME_DOCENTRY_UNIQUEID);
        public string PatientId => GetExternalIdentifier(XDSStrings.IDENTIFICATIONSCHEME_DOCENTRY_PATIENTID);
    }
}