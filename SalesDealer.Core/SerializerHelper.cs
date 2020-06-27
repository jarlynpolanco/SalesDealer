using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SalesDealer.Core
{
    public static class SerializerHelper
    {

        public static T StringToObject<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException("El xml es requerido, no puede estar en blanco.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            XmlReaderSettings settings = new XmlReaderSettings();

            using StringReader textReader = new StringReader(xml);
            using XmlReader xmlReader = XmlReader.Create(textReader, settings);
            return (T)serializer.Deserialize(xmlReader);
        }

        public static string ObjectToXml<T>(T value, bool addNameSpace = true)
        {
            if (value == null)
            {
                throw new ArgumentNullException("El objeto a serializar es requerido, no puede ser null.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "http://tempuri.org/");

            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    if (addNameSpace)
                        serializer.Serialize(xmlWriter, value, ns);
                    else
                        serializer.Serialize(xmlWriter, value);
                }
                return textWriter.ToString();
            }
        }

    }
}
