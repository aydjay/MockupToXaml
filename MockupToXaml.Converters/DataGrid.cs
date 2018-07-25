﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MockupToXaml.Model;
using System.Xml.Linq;

namespace MockupToXaml.Converters
{
    public class DataGrid : IMockupControlConverter
    {
        public DataGrid()
        {
        }
              
        public MockupTemplate Template { get; set; }

        public string ConvertMockupToXaml(MockupControl control)
        {
            string code = string.Empty;


            try
            {

                
                if (control.ControlProperties.ContainsKey("text"))
                {
                    string textValue = control.ControlProperties["text"];
                    if (!string.IsNullOrEmpty(textValue))
                    {
                        textValue = Uri.UnescapeDataString(textValue);
                        createFauxData(textValue);
                    }
                }

                code = Utility.PerformReplacements(Template.Template, control);

                // Add attributes for the mockup control properties
                XElement tag = XElement.Parse(code);

                // Check to see if this tag needs a namespace prefix.
                if (Template.Namespace.Contains("xmlns:"))
                {
                    if (Template.Namespace.Contains("="))
                    {
                        int eqOffset = Template.Namespace.IndexOf('=');
                        string prefix = Template.Namespace.Substring(0, eqOffset).Replace("xmlns:", "");

                        string tagXml = tag.ToString();
   
                        tagXml = tagXml.Replace("<", string.Format("<{0}:", prefix));
                        // correct the ending tags.. as the previous line hoarks them up.
                        tagXml = tagXml.Replace(string.Format("<{0}:/",prefix), string.Format("</{0}:", prefix));
                        return tagXml;
                    }
                }

                return tag.ToString();
            }
            catch
            {
                return code;
            }

        }

        private void createFauxData(string textValue)
        {
            List<string> lines = new List<string>(textValue.Replace("&#xA;", "\n").Split('\n'));
            // TODO: Read the Resource section of the template..  replace {Key} with a unique key and {Columns} with the data from the textValue.

            string key = string.Format("GridData_{0}", Guid.NewGuid());

            // Line 0 should be the column headers.
            string[] columns = lines[0].Split(',');
            
            // build the XML for the xml data provider
            XElement rows = new XElement("Rows");
            for (int i = 1; i < lines.Count; i++)
                rows.Add(createRow(lines[i].Split(',')));

            Template.Resource = Template.Resource.Replace("{DataProviderKey}", key)
                                    .Replace("{Rows}", rows.ToString());


            StringBuilder dataGridColumnsXml = new StringBuilder();
            dataGridColumnsXml.AppendLine("<DataGrid.Columns>");
            for (int i = 0; i < columns.Length; i++)
            {
                dataGridColumnsXml.AppendFormat("<DataGridTextColumn Header=\"{0}\" Binding=\"{2}Binding XPath=Column_{1}{3}\" />\r\n",
                    columns[i], i,"{","}");
            }
            dataGridColumnsXml.AppendLine("</DataGrid.Columns>");

            Template.Template = Template.Template.Replace("{DataProviderKey}", key)
                                    .Replace("{DataGridColumns}", dataGridColumnsXml.ToString());
        }

        private XElement createRow(string[] columns)
        {
            XElement row = new XElement("Row");
            for (int i = 0; i < columns.Length; i++)
            {
                XElement column = new XElement(string.Format("Column_{0}", i), columns[i]);
                row.Add(column);
            }

            return row;    
        }
    }
}
