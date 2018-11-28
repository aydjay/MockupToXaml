using System.Xml.Linq;

namespace MockupToXaml.Model
{
    public class Utility
    {
        public static void ProcessProperty(XElement tag, string xamlAttributeName, string mockupPropertyName, MockupControl mockupControl)
        {
            if (!string.IsNullOrEmpty(mockupControl.ControlProperties[mockupPropertyName]))
            {
                // TODO: check first to see if the content attr already exists.
                tag.SetAttributeValue(xamlAttributeName, mockupControl.ControlProperties[mockupPropertyName]);
            }
        }

        public static string PerformReplacements(string stringIn, MockupControl control)
        {
            var newString = stringIn;

            foreach (var pi in control.GetType().GetProperties())
            {
                newString = newString.Replace(string.Format("{1}{0}{2}", pi.Name, "{", "}"), pi.GetValue(control, null) != null ? pi.GetValue(control, null).ToString() : "");
            }

            return newString;
        }
    }
}