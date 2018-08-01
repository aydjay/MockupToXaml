using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MockupToXaml.Model
{
    public class MockupTemplate
    {
        public string MockupControlType { get; set; }
        public string Namespace { get; set; }
        public string ConverterClassName { get; set; }
        public string Template { get; set; }

        /// <summary>
        ///     Used to provide resource XAML that will be inserted in the parent's .Resources section.
        /// </summary>
        public string Resource { get; set; }

        public static MockupTemplate LoadFromXMLString(string xmlData)
        {
            MockupTemplate template = null;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xmlData)))
            {
                var xer = new XmlSerializer(typeof(MockupTemplate));

                template = (MockupTemplate) xer.Deserialize(ms);

                ms.Close();
                ms.Dispose();
            }

            return template;
        }

        public static MockupTemplate LoadFromXML(string filePath)
        {
            var xer = new XmlSerializer(typeof(MockupTemplate));
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var mockupTemplate = (MockupTemplate) xer.Deserialize(fs);

            fs.Close();
            fs.Dispose();

            return mockupTemplate;
        }
    }
}