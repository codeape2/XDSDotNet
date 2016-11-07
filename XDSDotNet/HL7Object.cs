using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XDSDotNet
{
    public class HL7Attribute : Attribute
    {
        public int Sequence { get; set; }
    }
    public abstract class HL7Object
    {
        internal class PropertyAndAttribute
        {
            public PropertyInfo Property;
            public HL7Attribute HL7Attribute;
        }

        public string Serialize()
        {
            var properties =
                from prop in GetType().GetProperties()
                let hl7Attrs = prop.GetCustomAttributes(typeof(HL7Attribute), true)
                where hl7Attrs.Length == 1
                orderby ((HL7Attribute)hl7Attrs[0]).Sequence
                select new { property = prop, hl7Attr = (HL7Attribute)hl7Attrs[0] };

            var retval = "";
            foreach (var item in GetHL7Properties(this))
            {
                retval += (string)item.Property.GetGetMethod().Invoke(this, null) + "^";
            }
            retval = Regex.Replace(retval, @"\^+$", "");
            return retval;
        }

        static private IEnumerable<PropertyAndAttribute> GetHL7Properties(HL7Object instance)
        {
            var retval =
                from prop in instance.GetType().GetProperties()
                let hl7Attrs = prop.GetCustomAttributes(typeof(HL7Attribute), true)
                where hl7Attrs.Length == 1
                orderby ((HL7Attribute)hl7Attrs[0]).Sequence
                select new PropertyAndAttribute { Property = prop, HL7Attribute = (HL7Attribute)hl7Attrs[0] };
            var expectedSequence = 1;
            foreach (var item in retval)
            {
                Debug.Assert(item.HL7Attribute.Sequence == expectedSequence++);
            }

            return retval;
        }

        static public T Parse<T>(string s) where T : HL7Object, new()
        {
            var retval = new T();
            var parts = s.Split('^');
            foreach (var item in GetHL7Properties(retval))
            {
                string value = null;
                if (item.HL7Attribute.Sequence - 1 <= parts.Length -1)
                {
                    value = parts[item.HL7Attribute.Sequence - 1];
                    if (value == "")
                    {
                        value = null;
                    }
                }
                item.Property.GetSetMethod().Invoke(retval, new object[] { value });
            }
            return retval;
        }
    }

    public class XON : HL7Object
    {
        [HL7(Sequence = 1)]
        public string InstitutionName { get; set; }
    }

    public class XCN : HL7Object
    {
        [HL7(Sequence = 1)]
        public string Identifier { get; set; }

        [HL7(Sequence = 2)]
        public string LastName { get; set; }

        [HL7(Sequence = 3)]
        public string FirstName { get; set; }

    }
}
