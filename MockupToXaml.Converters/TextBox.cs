using System.Xml.Linq;
using MockupToXaml.Model;

namespace MockupToXaml.Converters
{
    public class TextBox : IMockupControlConverter
    {
        public MockupTemplate Template { get; set; }

        public string ConvertMockupToXaml(MockupControl control)
        {
            var code = string.Empty;

            code = Utility.PerformReplacements(Template.Template, control);

            try
            {
                // Add attributes for the mockup control properties
                var tag = XElement.Parse(code);
                Utility.ProcessProperty(tag, "Text", "text", control);

                return tag.ToString();
            }
            catch
            {
                return code;
            }
        }
    }
}