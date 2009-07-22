using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NHibDataFoundation.Helpers
{
    public static class XmlSerializationHelper
    {
        public static string Serialize(object obj)
        {            
            var writer = new StringWriter();
            new XmlSerializer(obj.GetType()).Serialize(writer, obj);
            
            return writer.ToString();
        }

        public static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof (T));
            var reader = new StringReader(xml);
            var xmlReader = XmlReader.Create(reader);

            T result = (T)serializer.Deserialize(xmlReader);

            return result;
        }
    }
}