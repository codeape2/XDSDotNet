using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDSDotNet;
using static XDSDotNet.XMLNamespaces;


namespace XDSDotNet
{
    public class ProvideAndRegisterDocumentSet_ITI41
    {
        /// <summary>
        /// If null, CreateRequestBody() generates a unique ID automatically
        /// </summary>
        public string SubmissionSetId { get; set; }

        /// <summary>
        /// If null, CreateRequestBody() generates a unique ID automatically
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// If default value (DateTime.MinValue) uses current time
        /// </summary>
        public DateTime SubmissionTime { get; set; } = DateTime.MinValue;

        public string PatientId { get; private set; }
        public string SourceId { get; private set; }
        public string Name { get; private set; }
        public string MimeType { get; private set; }
        public byte[] DocumentContents { get; private set; }

        public string AuthorPerson { get; set; }
        public string[] AuthorInstitutions { get; set; }

        private List<XDSClassification> documentClassification = new List<XDSClassification>();
        private List<XDSClassification> submissionSetClassification = new List<XDSClassification>();
        private List<XDSSlot> documentSlots = new List<XDSSlot>();

        public ProvideAndRegisterDocumentSet_ITI41(string patientId, string sourceId, string name, string mimeType, byte[] documentContents)
        {
            PatientId = patientId;
            SourceId = sourceId;
            Name = name;
            MimeType = mimeType;
            DocumentContents = documentContents;
        }

        public IEnumerable<XDSClassification> DocumentClassification => documentClassification;
        public IEnumerable<XDSClassification> SubmissionSetClassification => submissionSetClassification;
        public IEnumerable<XDSSlot> DocumentSlots => documentSlots;


        public void AddDocumentClassification(params XDSClassification[] classifications)
        {
            Debug.Assert(classifications != null);
            documentClassification.AddRange(classifications);
        }

        public void AddSubmissionSetClassification(params XDSClassification[] classifications)
        {
            Debug.Assert(classifications != null);
            submissionSetClassification.AddRange(classifications);
        }

        public void AddDocumentSlot(params XDSSlot[] slots)
        {
            Debug.Assert(slots != null);
            documentSlots.AddRange(slots);
        }

        private XElement CreateAuthorClassification()
        {
            Debug.Assert(AuthorPerson != null || AuthorInstitutions != null);
            Debug.Assert(DocumentId != null);
            var classificationElement = new XElement(rim + "Classification", 
                Requests.Attributes("classifiedObject", DocumentId, "classificationScheme", XDSStrings.CLASSIFICATIONSCHEME_AUTHOR, "nodeRepresentation", "author")
            );
            if (AuthorPerson != null)
            {
                classificationElement.Add(Requests.CreateSlot("authorPerson", AuthorPerson));
            }

            if (AuthorInstitutions != null && AuthorInstitutions.Length > 0)
            {
                classificationElement.Add(Requests.CreateSlot("authorInstitution", AuthorInstitutions));
            }
            return classificationElement;
        }

        public XElement CreateRequestBody()
        {
            var submissionSetId = SubmissionSetId ?? Guid.NewGuid().ToString();
            DocumentId = DocumentId ?? Guid.NewGuid().ToString();
            var classificationElements = (from dc in documentClassification select CreateClassification(DocumentId, dc)).ToList();

            var documentClassificationElements = (from dc in documentClassification select CreateClassification(DocumentId, dc)).ToList();
            if (AuthorPerson != null || AuthorInstitutions != null)
            {
                documentClassificationElements.Add(CreateAuthorClassification());
            }
            
            var submissionTime = SubmissionTime == DateTime.MinValue ? DateTime.Now : SubmissionTime;
            return new XElement(
                xds + "ProvideAndRegisterDocumentSetRequest",
                new XElement(
                    lcm + "SubmitObjectsRequest",
                    new XElement(
                        rim + "RegistryObjectList",
                        new XElement(
                            rim + "ExtrinsicObject",
                            Requests.Attributes("id", DocumentId, "objectType", "urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1", "mimeType", MimeType),
                            new XElement(rim + "Name", new XElement(rim + "LocalizedString", new XAttribute("value", Name))),
                            new XElement(rim + "ExternalIdentifier", Requests.Attributes("registryObject", DocumentId, "identificationScheme", "urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab", "value", DocumentId)),
                            Requests.CreateSlot("sourcePatientId", PatientId),
                            from ds in documentSlots select Requests.CreateSlot(ds.Name, ds.Value),
                            documentClassificationElements,
                            new XElement(rim + "ExternalIdentifier", Requests.Attributes("registryObject", DocumentId, "identificationScheme", "urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427", "value", PatientId))
                        ),
                        new XElement(
                            rim + "RegistryPackage",
                            new XAttribute("id", submissionSetId),
                            Requests.CreateSlot("submissionTime", submissionTime.ToString("yyyyMMddHHmmss")),
                            from ssc in submissionSetClassification select CreateClassification(submissionSetId, ssc),
                            new XElement(
                                rim + "ExternalIdentifier",
                                Requests.Attributes("registryObject", submissionSetId, "identificationScheme", "urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446", "value", PatientId)
                            ),
                            new XElement(
                                rim + "ExternalIdentifier",
                                Requests.Attributes("registryObject", submissionSetId, "identificationScheme", "urn:uuid:554ac39e-e3fe-47fe-b233-965d2a147832", "value", SourceId)
                            ),
                             new XElement(rim + "ExternalIdentifier", Requests.Attributes("registryObject", submissionSetId, "identificationScheme", "urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8", "value", submissionSetId))
                        ),
                        new XElement(
                            rim + "Classification",
                            new XAttribute("classifiedObject", submissionSetId),
                            new XAttribute("classificationNode", "urn:uuid:a54d6aa5-d40d-43f9-88c5-b4633d873bdd")
                        ),
                        new XElement(
                            rim + "Association",
                            Requests.Attributes("associationType", "urn:oasis:names:tc:ebxml-regrep:AssociationType:HasMember", "sourceObject", submissionSetId, "targetObject", DocumentId, "objectType", "urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:Association"),
                            Requests.CreateSlot("SubmissionSetStatus", "Original")
                        )
                    )
                ),
                new XElement(
                    xds + "Document",
                    Requests.Attributes("id", DocumentId),
                    Convert.ToBase64String(DocumentContents)
                )
            );

        }

        static public XElement CreateClassification(string classifiedObject, string classificationScheme, string nodeRepresentation, string codingScheme, string name)
        {
            return new XElement(
                rim + "Classification",
                new XAttribute("classifiedObject", classifiedObject),
                new XAttribute("classificationScheme", classificationScheme),
                new XAttribute("nodeRepresentation", nodeRepresentation),
                Requests.CreateSlot("codingScheme", codingScheme),
                new XElement(rim + "Name", new XElement(rim + "LocalizedString", new XAttribute("value", name)))
            );
        }

        static public XElement CreateClassification(string classifiedObject, XDSClassification classification)
        {
            return CreateClassification(classifiedObject, classification.ClassificationScheme, classification.NodeRepresentation, classification.CodingScheme, classification.Name);
        }

    }
}
