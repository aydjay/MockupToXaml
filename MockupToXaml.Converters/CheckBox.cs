using System;
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

                if (control.ControlProperties.ContainsKey("state"))
                {
                    CheckBoxStateEnum result;
                    Enum.TryParse(control.ControlProperties["state"], true, out result);

                    switch (result)
                    {
                        case CheckBoxStateEnum.Selected:
                            tag.SetAttributeValue("IsChecked", true);
                            break;
                        case CheckBoxStateEnum.Disabled:
                            tag.SetAttributeValue("IsEnabled", false);
                            break;
                        case CheckBoxStateEnum.DisabledSelected:
                            tag.SetAttributeValue("IsEnabled", false);
                            tag.SetAttributeValue("IsChecked", true);
                            break;
                        default:
                            tag.SetAttributeValue("Content", "Unsupported Checkbox State");
                            break;
                    }
                }

                return tag.ToString();
            }
            catch
            {
                return code;
            }
        }

        private enum CheckBoxStateEnum
        {
            Unknown,
            Selected,
            Disabled,
            DisabledSelected,
        }
    }
}