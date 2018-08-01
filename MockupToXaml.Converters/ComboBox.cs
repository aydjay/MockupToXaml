using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MockupToXaml.Model;

namespace MockupToXaml.Converters
{
    public class ComboBox : IMockupControlConverter
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

                if (control.ControlProperties.ContainsKey("text"))
                {
                    string textValue = control.ControlProperties["text"];
                    if (!string.IsNullOrEmpty(textValue))
                    {
                        textValue = Uri.UnescapeDataString(textValue);
                        var listItems = new List<string>(textValue.Split('\n'));
                        foreach (var listItem in listItems)
                        {
                            // Add the listItem to the listbox control.
                            var xListItem = new XElement("ComboBoxItem");
                            xListItem.SetAttributeValue("Content", listItem);
                            tag.Add(xListItem);
                        }
                    }
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