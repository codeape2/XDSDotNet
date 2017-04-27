using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using static XDSDotNet.XMLNamespaces;
using static XDSDotNet.XDSStrings;

namespace XDSDotNet
{
    public static class Requests
    {
        static public XElement GetAll_ITI18(string patientId)
        {
            return new XElement(query + "AdhocQueryRequest",
                new XElement(query + "ResponseOption",
                    new XAttribute("returnComposedObjects", true),
                    new XAttribute("returnType", LEAF_CLASS)
                ),
                new XElement(rim + "AdhocQuery",
                    new XAttribute("id", STORED_QUERY_GET_ALL),
                    CreateSlot(QRY_PATIENT_ID, QueryString(patientId)),
                    CreateSlot(QRY_DOCUMENT_ENTRY_STATUS, QueryString(DEFAULT_STATI)),
                    CreateSlot(QRY_SUBMISSION_SET_STATUS, QueryString(DEFAULT_STATI)),
                    CreateSlot(QRY_FOLDER_STATUS, QueryString(DEFAULT_STATI))
                )
            );
        }

        static public XElement FindDocuments_ITI18(string patientId)
        {
            return new XElement(query + "AdhocQueryRequest",
                new XElement(query + "ResponseOption",
                    new XAttribute("returnComposedObjects", true),
                    new XAttribute("returnType", LEAF_CLASS)
                ),
                new XElement(rim + "AdhocQuery",
                    new XAttribute("id", STORED_QUERY_FIND_DOCUMENTS),
                    CreateSlot(QRY_DOCUMENT_ENTRY_PATIENT_ID, QueryString(patientId)),
                    CreateSlot(QRY_DOCUMENT_ENTRY_STATUS, QueryString(DEFAULT_STATI))
                )
            );
        }


        static public XElement GetSingleDocument_ITI43(string repositoryUniqueId, string documentUniqueId, string homeCommunityId = null)
        {
            var retval = new XElement(
                xds + "RetrieveDocumentSetRequest",
                new XElement(
                    xds + "DocumentRequest",
                    new XElement(
                        xds + "RepositoryUniqueId",
                        repositoryUniqueId
                    ),
                    new XElement(
                        xds + "DocumentUniqueId",
                        documentUniqueId
                    ),
                    homeCommunityId != null
                        ?
                            new XElement(
                                xds + "HomeCommunityId", 
                                homeCommunityId
                            )
                        :
                            null
                )
            );
            return retval;
        }

        public static IEnumerable<XAttribute> Attributes(params string[] items)
        {
            var retval = new List<XAttribute>();
            for (var i = 0; i < items.Length; i += 2)
            {
                retval.Add(new XAttribute(items[i], items[i + 1]));
            }
            return retval;
        }


        static public XElement CreateSlot(string name, string value)
        {
            return CreateSlot(name, new[] { value });
        }

        static public XElement CreateSlot(string name, string[] values)
        {
            return new XElement(rim + "Slot",
                new XAttribute("name", name),
                new XElement(rim + "ValueList", from value in values select new XElement(rim + "Value", value))
            );
        }

        static public string QueryString(string input)
        {
            return $"'{input}'";
        }

        static public string QueryString(string[] items)
        {
            return "(" + string.Join(",", from item in items select QueryString(item)) + ")";
        }


    }
}