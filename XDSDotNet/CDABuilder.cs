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
        public DateTime EffectiveTime { get; set; } = DateTime.MinValue;
        public string IdRoot { get; set; }
        public string IdExtension { get; set; }
        public string PatientIdRoot { get; set; }
        public string PatientIdExtension { get; set; }
        public string AuthorIdRoot { get; set; }
        public string AuthorIdExtension { get; set; }
        public string PatientGivenName { get; set; }
        public string PatientFamilyName { get; set; }
        public string AuthorGivenName { get; set; }
        public string AuthorFamilyName { get; set; }
        public string OrganizationRoot { get; set; }
        public string OrganizationExtension { get; set; }

        public CDABuilder(string mimeType, byte[] contents)
        {
            _mimeType = mimeType;
            _contents = contents;
        }

        private void ensureValueProvided(string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Property {name} must be set");
            }
        }

        private XElement idElement(string root, string extension)
        {
            return new XElement(hl7 + "id", new XAttribute("root", root), new XAttribute("extension", extension));
        }

        private XElement nameElement(string given, string family)
        {
            return new XElement(hl7 + "name",
                given != null ? new XElement(hl7 + "given", given) : null,
                family != null ? new XElement(hl7 + "family", family) : null
            );
        }

        public XElement CreateCDA()
        {
            ensureValueProvided(IdRoot, nameof(IdRoot));
            ensureValueProvided(IdExtension, nameof(IdExtension));
            ensureValueProvided(EffectiveTime == DateTime.MinValue ? null : "", nameof(EffectiveTime));
            ensureValueProvided(PatientIdRoot, nameof(PatientIdRoot));
            ensureValueProvided(PatientIdExtension, nameof(PatientIdExtension));
            ensureValueProvided(AuthorIdRoot, nameof(AuthorIdRoot));
            ensureValueProvided(AuthorIdExtension, nameof(AuthorIdExtension));
            ensureValueProvided(OrganizationRoot, nameof(OrganizationRoot));
            ensureValueProvided(OrganizationExtension, nameof(OrganizationExtension));

            XElement patient = null;
            if (PatientGivenName != null || PatientFamilyName != null)
            {
                patient = new XElement(hl7 + "patient", nameElement(PatientGivenName, PatientFamilyName));
            }

            XElement assignedPerson = null;
            if (AuthorGivenName != null || AuthorFamilyName != null)
            {
                assignedPerson = new XElement(hl7 + "assignedPerson", nameElement(AuthorGivenName, AuthorFamilyName));
            }

            return new XElement(hl7 + "ClinicalDocument", 
                new XElement(hl7 + "typeId", new XAttribute("root", "2.16.840.1.113883.1.3"), new XAttribute("extension", "POCD_HD000040")),
                idElement(IdRoot, IdExtension),
                new XElement(hl7 + "code"),
                new XElement(hl7 + "effectiveTime", new XAttribute("value", EffectiveTime.ToString("yyyyMMddHHmmss"))),
                new XElement(hl7 + "confidentialityCode"),
                new XElement(hl7 + "recordTarget",
                    new XElement(hl7 + "patientRole",
                        idElement(PatientIdRoot, PatientIdExtension),
                        patient
                    )
                ),
                new XElement(hl7 + "author",
                    new XElement(hl7 + "time", new XAttribute("value", EffectiveTime.ToString("yyyyMMddHHmmss"))), 
                    new XElement(hl7 + "assignedAuthor",
                        idElement(AuthorIdRoot, AuthorIdExtension),
                        assignedPerson
                    )
                ),
                new XElement(hl7 + "custodian",
                    new XElement(hl7 + "assignedCustodian",
                        new XElement(hl7 + "representedCustodianOrganization",
                            idElement(OrganizationRoot, OrganizationExtension)
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
