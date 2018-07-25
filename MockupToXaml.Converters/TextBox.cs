﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MockupToXaml.Model;
using System.Xml.Linq;

namespace MockupToXaml.Converters
{
    public class TextBox : IMockupControlConverter
    {
        public TextBox()
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
                Utility.ProcessProperty(tag, "TextBox", "Text", "text", control);

                return tag.ToString();
            }
            catch
            {
                return code;
            }

        }
    }
}
