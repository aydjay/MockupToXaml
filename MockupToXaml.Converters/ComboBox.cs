using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MockupToXaml.Model;
using System.Xml.Linq;

namespace MockupToXaml.Converters
{
    public class ComboBox : IMockupControlConverter
    {
        public ComboBox()
        {
        }
        
        public MockupTemplate Template { get; set; }

        public string ConvertMockupToXaml(MockupControl control)
        {
            string code = string.Empty;

            code = Utility.PerformReplacements(Template.Template, control);

            try
            {
                // Add attributes for the mockup control properties
                XElement tag = XElement.Parse(code);

                if (control.ControlProperties.ContainsKey("text"))
                {
                    string textValue = control.ControlProperties["text"];
                    if (!string.IsNullOrEmpty(textValue))
                    {
                        textValue = Uri.UnescapeDataString(textValue);
                        List<string> listItems = new List<string>(textValue.Split('\n'));
                        foreach (string listItem in listItems)
                        {
                            // Add the listItem to the listbox control.
                            XElement xListItem = new XElement("ComboBoxItem");
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
