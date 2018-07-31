using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace MockupToXaml.Model
{
    public class Utility
    {

        public static void ProcessProperties(XElement tag, MockupControl control)
        {
            if (tag.Name.LocalName == "Button")
                ProcessProperty(tag, "Button", "Content", "text", control);

            if (tag.Name.LocalName == "TextBlock")
                ProcessProperty(tag, "TextBlock", "Text", "text", control);
        }

        public static void ProcessProperty(XElement tag, string xamlControlName, string xamlAttributeName, string mockupPropertyName, MockupControl mockupControl)
        {
            if (!string.IsNullOrEmpty(mockupControl.ControlProperties[mockupPropertyName]))
            {
                // TODO: check first to see if the content attr already exists.
                tag.SetAttributeValue(xamlAttributeName, mockupControl.ControlProperties[mockupPropertyName]);
            }
        }

        public static string PerformReplacements(string stringIn, MockupControl control)
        {
            string newString = stringIn;

            foreach (PropertyInfo pi in control.GetType().GetProperties())
            {
                newString = newString.Replace(string.Format("{1}{0}{2}", pi.Name, "{", "}"), pi.GetValue(control, null) != null ? pi.GetValue(control, null).ToString() : "");
            }

            return newString;
        }

    }
}
