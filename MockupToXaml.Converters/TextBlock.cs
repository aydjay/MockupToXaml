using System.Xml.Linq;
using MockupToXaml.Model;

namespace MockupToXaml.Converters
{
    public class TextBlock : IMockupControlConverter
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
                Utility.ProcessProperty(tag, "TextBlock", "Text", "text", control);

                return tag.ToString();
            }
            catch
            {
                return code;
            }
        }
    }
}