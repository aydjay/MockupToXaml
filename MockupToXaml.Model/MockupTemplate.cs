using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MockupToXaml.Model
{
    public class MockupTemplate
    {
        public MockupTemplate()
        {
        }

        public string MockupControlType { get; set; }
        public string Namespace { get; set; }
        public string ConverterClassName { get; set; }
        public string Template { get; set; }
        /// <summary>
        /// Used to provide resource XAML that will be inserted in the parent's .Resources section.
        /// </summary>
        public string Resource { get; set; }
        public static MockupTemplate LoadFromXMLString(string xmlData)
        {
            MockupTemplate template = null;

            using (MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(xmlData)))
            {
                XmlSerializer xer = new XmlSerializer(typeof(MockupTemplate));

                template = (MockupTemplate)xer.Deserialize(ms);

                ms.Close();
                ms.Dispose();
            }
            return template;
        }

        public static MockupTemplate LoadFromXML(string filePath)
        {

            XmlSerializer xer = new XmlSerializer(typeof(MockupTemplate));
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            MockupTemplate mockupTemplate = (MockupTemplate)xer.Deserialize(fs);

            fs.Close();
            fs.Dispose();

            return mockupTemplate;

        }
    }
}
