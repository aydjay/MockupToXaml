using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using MockupToXaml.Model;

namespace MockupToXaml.Local
{
    public class XamlGenerator
    {
        public MockupHolder MockupHolder { get; set; }

        public string GeneratedCode { get; set; }

        public string NamespaceHeader { get; set; }

        public string ResourceHeader { get; set; }

        public void Generate()
        {
            var code = new StringBuilder();
            NamespaceHeader = "";
            ResourceHeader = "";

            code.AppendFormat("<Grid Height=\"{0}\" Width=\"{1}\">\r\n", MockupHolder.Mockup.Height, MockupHolder.Mockup.Width);

            foreach (var control in MockupHolder.Mockup.Controls.Control)
            {
                code.AppendLine(getXaml(control));
            }

            code.AppendLine("</Grid>");

            GeneratedCode = FormatXml(code.ToString());
        }
        
        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        private string getXaml(MockupControl control)
        {
            var templateName = control.ControlTypeId.Replace(':', '_');
            string templateFilename;

            var uri = new Uri(Assembly.GetEntryAssembly().CodeBase);
            var exeBasePath = Path.GetDirectoryName(uri.LocalPath);

            if (File.Exists(string.Format("{1}\\Templates\\{0}.xml", templateName, exeBasePath)))
            {
                templateFilename = string.Format("{1}\\Templates\\{0}.xml", templateName, exeBasePath);
            }
            else
            {
                templateFilename = exeBasePath + "\\Templates\\unknown.xml";
            }

            var template = MockupTemplate.LoadFromXML(templateFilename);


            var converterAssembly = Assembly.LoadFile(Path.Combine(exeBasePath, "MockupToXaml.Converters.dll"));
            var converter = (IMockupControlConverter) converterAssembly.CreateInstance(template.ConverterClassName);

            converter.Template = template;

            var code = converter.ConvertMockupToXaml(control);

            if (!NamespaceHeader.Contains(converter.Template.Namespace))
            {
                NamespaceHeader += converter.Template.Namespace + "\r\n";
            }

            if (!string.IsNullOrEmpty(converter.Template.Resource))
            {
                ResourceHeader += converter.Template.Resource + "\r\n";
            }

            return code;
        }
    }
}