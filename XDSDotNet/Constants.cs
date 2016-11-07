using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace XDSDotNet
{
    public static class XMLNamespaces
    {
        static public XNamespace query =    "urn:oasis:names:tc:ebxml-regrep:xsd:query:3.0";
        static public XNamespace rim =      "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0";
        static public XNamespace hl7 =      "urn:hl7-org:v3";
        static public XNamespace xds =      "urn:ihe:iti:xds-b:2007";
        static public XNamespace lcm =      "urn:oasis:names:tc:ebxml-regrep:xsd:lcm:3.0";
        static public XNamespace rs =       "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";
        //etc
    }

    public static class XMLTagAndAttributeNames
    {
        static public XName EXTRINSICOBJECT = XMLNamespaces.rim + "ExtrinsicObject";
        static public XName EXTERNALIDENTIFIER = XMLNamespaces.rim + "ExternalIdentifier";
        static public XName CLASSIFICATION = XMLNamespaces.rim + "Classification";
    }

    public static class XDSStrings
    {
        public const string STORED_QUERY_FIND_DOCUMENTS = "urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d";
        public const string STORED_QUERY_FIND_SUBMISSIONSETS = "urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9";
        public const string STORED_QUERY_FIND_FOLDERS = "urn:uuid:958f3006-baad-4929-a4de-ff1114824431";
        public const string STORED_QUERY_GET_ALL = "urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3";
        public const string STORED_QUERY_GET_DOCUMENTS = "urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4";
        public const string STORED_QUERY_GET_FOLDERS = "urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4";
        public const string STORED_QUERY_GET_ASSOC = "urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155";
        public const string STORED_QUERY_GET_DOC_AND_ASSOC = "urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a";
        public const string STORED_QUERY_GET_SUBMISSIONSETS = "urn:uuid:51224314-5390-4169-9b91-b1980040715a";
        public const string STORED_QUERY_GET_SUBMISSIONSETS_AND_CONTENT = "urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83";
        public const string STORED_QUERY_GET_FOLDER_AND_CONTENT = "urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7";
        public const string STORED_QUERY_GET_FOLDER_FOR_DOC = "urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578";
        public const string STORED_QUERY_GET_RELATED_DOCS = "urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6";

        public const string QRY_DOCUMENT_ENTRY_PATIENT_ID = "$XDSDocumentEntryPatientId";
        public const string QRY_DOCUMENT_ENTRY_STATUS = "$XDSDocumentEntryStatus";
        public const string QRY_FOLDER_STATUS = "$XDSFolderStatus";
        public const string QRY_FOLDER_ENTRY_UUID = "$XDSFolderEntryUUID";
        public const string QRY_SUBMISSION_SET_STATUS = "$XDSSubmissionSetStatus";
        public const string QRY_SUBMISSION_SET_PATIENT_ID = "$XDSSubmissionSetPatientId";
        public const string QRY_SUBMISSION_SET_ENTRY_UUID = "$XDSSubmissionSetEntryUUID";
        public const string QRY_PATIENT_ID = "$patientId";
        public const string QRY_FOLDER_PATIENT_ID = "$XDSFolderPatientId";
        public const string QRY_UUID = "$uuid";
        public const string QRY_DOCUMENT_ENTRY_ENTRY_UUID = "$XDSDocumentEntryEntryUUID";
        public const string QRY_DOCUMENT_ENTRY_UNIQUE_ID = "$XDSDocumentEntryUniqueId";
        public const string QRY_ASSOCIATION_TYPES = "$AssociationTypes";

        public const string SUBMITTED = "urn:oasis:names:tc:ebxml-regrep:StatusType:Submitted";
        public const string APPROVED = "urn:oasis:names:tc:ebxml-regrep:StatusType:Approved";
        public const string DEPRECATED = "urn:oasis:names:tc:ebxml-regrep:StatusType:Deprecated";

        static readonly public string[] DEFAULT_STATI = new[] { SUBMITTED, APPROVED, DEPRECATED };


        public const string LEAF_CLASS = "LeafClass";

        public const string IDENTIFICATIONSCHEME_DOCENTRY_UNIQUEID = "urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab";
        public const string IDENTIFICATIONSCHEME_DOCENTRY_PATIENTID = "urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427";

        public const string CLASSIFICATIONSCHEME_AUTHOR =                     "urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d";
        public const string CLASSIFICATIONSCHEME_CLASSCODE =                  "urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a";
        public const string CLASSIFICATIONSCHEME_CONFIDENTIALITYCODE =        "urn:uuid:f4f85eac-e6cb-4883-b524-f2705394840f";
        public const string CLASSIFICATIONSCHEME_EVENTCODELIST =              "urn:uuid:2c6b8cb7-8b2a-4051-b291-b1ae6a575ef4";
        public const string CLASSIFICATIONSCHEME_FORMATCODE =                 "urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d";
        public const string CLASSIFICATIONSCHEME_HEALTHCAREFACILITYTYPECODE = "urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1";
        public const string CLASSIFICATIONSCHEME_PRACTICESETTINGCODE =        "urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead";
        public const string CLASSIFICATIONSCHEME_TYPECODE =                   "urn:uuid:f0306f51-975f-434e-a61c-c59651d33983";

        public static string[] KnownClassificationSchemes = new[] {
            CLASSIFICATIONSCHEME_AUTHOR,
            CLASSIFICATIONSCHEME_CLASSCODE,
            CLASSIFICATIONSCHEME_CONFIDENTIALITYCODE,
            CLASSIFICATIONSCHEME_EVENTCODELIST,
            CLASSIFICATIONSCHEME_FORMATCODE,
            CLASSIFICATIONSCHEME_HEALTHCAREFACILITYTYPECODE,
            CLASSIFICATIONSCHEME_PRACTICESETTINGCODE,
            CLASSIFICATIONSCHEME_TYPECODE
        };

        public const string CLASSIFICATIONSCHEME_SUBMISSIONSET_CONTENTTYPECODE = "urn:uuid:aa543740-bdda-424e-8c96-df4873be8500";

        public const string RESPONSSTATUSTYPE_FAILURE = "urn:oasis:names:tc:ebxml-regrep:ResponseStatusType:Failure";
        public const string RESPONSSTATUSTYPE_SUCCESS = "urn:oasis:names:tc:ebxml-regrep:ResponseStatusType:Success";
    }
}

/*

URL: 
    
urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1	XDSDocumentEntry	ClassificationNode	R/R
urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d	XDSDocumentEntry.author	External Classification Scheme	R2/R
urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a	XDSDocumentEntry.classCode	External Classification Scheme	R/R
urn:uuid:f4f85eac-e6cb-4883-b524-f2705394840f	XDSDocumentEntry.confidentialityCode	External Classification Scheme	R/P
urn:uuid:2c6b8cb7-8b2a-4051-b291-b1ae6a575ef4	XDSDocumentEntry.eventCodeList	External Classification Scheme	O/R
urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d	XDSDocumentEntry.formatCode	External Classification Scheme	R/R
urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1	XDSDocumentEntry.healthcareFacilityTypeCode	External Classification Scheme	R/R
urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427	XDSDocumentEntry.patientId	ExternalIdentifier	R/R
urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead	XDSDocumentEntry.practiceSettingCode	External Classification Scheme	R/R
urn:uuid:f0306f51-975f-434e-a61c-c59651d33983	XDSDocumentEntry.typeCode	External Classification Scheme	R/R
urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab	XDSDocumentEntry.uniqueId	ExternalIdentifier	R/R
urn:uuid:ab9b591b-83ab-4d03-8f5d-f93b1fb92e85	XDSDocumentEntry.limitedMetadata	ClassificationNode	Metadata Limited


 
*/
