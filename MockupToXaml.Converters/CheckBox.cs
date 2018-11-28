using System.Xml.Linq;
using MockupToXaml.Model;

namespace MockupToXaml.Converters
{
    public class CheckBox : IMockupControlConverter
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
                Utility.ProcessProperty(tag, "Content", "text", control);
                //Todo Handle state
                if (control.ControlProperties.ContainsKey("state"))
                {

                }

                return tag.ToString();
            }
            catch
            {
                return code;
            }
        }
    }
}