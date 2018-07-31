using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MockupToXaml.Model;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace MockupToXaml.Local
{
    public class XamlGenerator
    {
        public Mockup Mockup { get; set; }

        public string GeneratedCode { get; set; }

        public string NamespaceHeader { get; set; }

        public string ResourceHeader { get; set; }

        public void Generate()
        {
            StringBuilder code = new StringBuilder();
            NamespaceHeader = "";
            ResourceHeader = "";

            code.AppendFormat("<Grid Height=\"{0}\" Width=\"{1}\">\r\n", Mockup.Height, Mockup.Width);

            foreach (MockupControl control in Mockup.Controls)
            {
                code.AppendLine(getXaml(control));
            }

            code.AppendLine("</Grid>");

            GeneratedCode = code.ToString();
        }

        #region deprecated code
        ///// <summary>
        ///// DEPRECATE THIS METHOD
        ///// </summary>
        ///// <param name="control"></param>
        ///// <returns></returns>
        //private string getXamlOld(MockupControl control)
        //{
        //    StringBuilder code = new StringBuilder();
            
        //    string templateName = control.ControlTypeID.Replace(':', '_');
        //    string templateFilename;

        //    Uri uri = new Uri(Assembly.GetEntryAssembly().CodeBase);
        //    string exeBasePath = uri.AbsolutePath.Replace("/", "\\");
        //    exeBasePath = Path.GetDirectoryName(exeBasePath);

        //    if (File.Exists(string.Format("{1}\\Templates\\{0}.txt", templateName, exeBasePath)))
        //    {
        //        templateFilename = string.Format("{1}\\Templates\\{0}.txt", templateName, exeBasePath);
        //    }
        //    else
        //        templateFilename = exeBasePath + "\\Templates\\unknown.txt";

        //    List<string> templateLines = new List<string>(File.ReadAllLines(templateFilename));

        //    if (!NamespaceHeader.Contains(templateLines[0]))
        //        NamespaceHeader += templateLines[0] + "\r\n";

        //    for (int i = 1; i < templateLines.Count; i++)
        //        code.AppendLine(performReplacements(templateLines[i], control));

        //    try
        //    {
        //        // Add attributes for the mockup control properties
        //        XElement tag = XElement.Parse(code.ToString());
        //        processProperties(tag, control);

        //        return tag.ToString();
        //    }
        //    catch
        //    {
        //        return code.ToString();
        //    }
        //}
        //private void processProperties(XElement tag, MockupControl control)
        //{
        //    if (tag.Name.LocalName == "Button")
        //        processProperty(tag, "Button", "Content", "text", control);

        //    if (tag.Name.LocalName == "TextBlock")
        //        processProperty(tag, "TextBlock", "Text", "text", control);



        //}

        //private void processProperty(XElement tag, string xamlControlName, string xamlAttributeName, string mockupPropertyName, MockupControl mockupControl)
        //{
        //    if (!string.IsNullOrEmpty(mockupControl.ControlProperties[mockupPropertyName]))
        //    {
        //        // TODO: check first to see if the content attr already exists.
        //        tag.SetAttributeValue(xamlAttributeName, mockupControl.ControlProperties[mockupPropertyName]);
        //    }
        //}

        //private string performReplacements(string stringIn, MockupControl control)
        //{
        //    string newString = stringIn;

        //    foreach (PropertyInfo pi in control.GetType().GetProperties())
        //    {
        //        newString = newString.Replace(string.Format("{1}{0}{2}", pi.Name, "{", "}"), pi.GetValue(control, null) != null ? pi.GetValue(control, null).ToString() : "");
        //    }

        //    return newString;
        //}
        #endregion

         private string getXaml(MockupControl control)
        {
            
            string templateName = control.ControlTypeID.Replace(':', '_');
            string templateFilename;
            string pluginAssemblyPath;

            Uri uri = new Uri(Assembly.GetEntryAssembly().CodeBase);
            string exeBasePath = Path.GetDirectoryName(uri.LocalPath);

            if (File.Exists(string.Format("{1}\\Templates\\{0}.xml", templateName, exeBasePath)))
            {
                templateFilename = string.Format("{1}\\Templates\\{0}.xml", templateName, exeBasePath);
            }
            else
                templateFilename = exeBasePath + "\\Templates\\unknown.xml";

            MockupTemplate template = MockupTemplate.LoadFromXML(templateFilename);
            
                        
            Assembly converterAssembly = Assembly.LoadFile(Path.Combine(exeBasePath, "MockupToXaml.Converters.dll"));
            IMockupControlConverter converter = (IMockupControlConverter)converterAssembly.CreateInstance(template.ConverterClassName);

            converter.Template = template;

            string code = converter.ConvertMockupToXaml(control);
            
             if (!NamespaceHeader.Contains(converter.Template.Namespace))
                NamespaceHeader += converter.Template.Namespace + "\r\n";

             if (!string.IsNullOrEmpty(converter.Template.Resource))
                 ResourceHeader += converter.Template.Resource + "\r\n";

            return code;
        }
    }
}
