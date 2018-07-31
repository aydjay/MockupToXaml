using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MockupToXaml.Model
{
    public class MockupHolder
    {
        [JsonProperty("mockup")]
        public Mockup Mockup { get; set; }

        public static MockupHolder DeserialiseFile(string filePath)
        {
            // TODO: validation, error checking.
            return JsonConvert.DeserializeObject<MockupHolder>(File.ReadAllText(filePath));
        }
    }

    public class Controls
    {
        public List<MockupControl> Control { get; set; }
    }

    public class Mockup
    {
        [JsonProperty("mockupW")]
        public int Width { get; set; }

        [JsonProperty("mockupH")]
        public int Height { get; set; }

        [JsonProperty("controls")]
        public Controls Controls { get; set; }



        ///// <summary>
        /////     Extracts the control properties from the raw XML document that represents the mockup (BMML) file.
        ///// </summary>
        ///// <param name="xmlDocument">Raw XML mockup document (BMML) file.</param>
        //internal void extractControlProperties(string rawXmlDocument)
        //{
        //    var xdoc = XElement.Parse(rawXmlDocument);


        //    foreach (var control in Controls)
        //    {
        //        var controls = xdoc.Descendants("controls");
        //        var xcontrol = controls.Descendants("control").FirstOrDefault(x => x.Element("ID").Value == control.ControlId.ToString());
        //        if (xcontrol == null)
        //        {
        //            // TODO: Warn? Log? we should not just ignore this.. controlID should have been found.
        //            continue;
        //        }

        //        control.ControlProperties = new Dictionary<string, string>();

        //        foreach (var xcontrolProp in xcontrol.Elements("properties").Elements())
        //        {
        //            //For now - only handle "Simple" properties
        //            if (xcontrolProp.HasElements)
        //            {
        //                //TODO: Implement some logging
        //                continue;
        //            }

        //            if (!string.IsNullOrEmpty(xcontrolProp.Value))
        //            {
        //                control.ControlProperties.Add(xcontrolProp.Name.LocalName, Uri.UnescapeDataString(xcontrolProp.Value));
        //            }
        //        }
        //    }
        //}
    }
}