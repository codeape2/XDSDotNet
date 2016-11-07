using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static XDSDotNet.XMLNamespaces;

namespace XDSDotNet
{
    public class CDABuilder
    {
        private string _mimeType;
        private byte[] _contents;

        public string Title { get; set; }
        public DateTime EffectiveTime { get; set; }

        public CDABuilder(string mimeType, byte[] contents)
        {
            _mimeType = mimeType;
            _contents = contents;
        }

        public XElement CreateCDA()
        {
            return new XElement(hl7 + "ClinicalDocument", 
                new XElement(hl7 + "typeId", new XAttribute("root", "2.16.840.1.113883.1.3"), new XAttribute("extension", "POCD_HD000040")),
                new XElement(hl7 + "id", new XAttribute("extension", "TODO")), //document id
                new XElement(hl7 + "code"),
                new XElement(hl7 + "effectiveTime"),
                new XElement(hl7 + "confidentialityCode"),
                new XElement(hl7 + "recordTarget",
                    new XElement(hl7 + "patientRole",
                        new XElement(hl7 + "id", new XAttribute("extension", "TODO")) // patient id
                    )
                ),
                new XElement(hl7 + "author",
                    new XElement(hl7 + "time"), //TOOD
                    new XElement(hl7 + "assignedAuthor",
                        new XElement(hl7 + "id", new XAttribute("extension", "TODO")) // author id og legg til assignedPerson evt.
                    )
                ),
                new XElement(hl7 + "custodian",
                    new XElement(hl7 + "assignedCustodian",
                        new XElement(hl7 + "representedCustodianOrganization",
                            new XElement(hl7 + "id", new XAttribute("extension", "TODO")) // organisasjon
                        )
                    )
                ),
                new XElement(hl7 + "component",
                    new XElement(hl7 + "nonXMLBody",
                        new XElement(hl7 + "text", new XAttribute("mediaType", _mimeType), new XAttribute("representation", "B64"), Convert.ToBase64String(_contents))
                    )
                )
            );
        }
    }
}
