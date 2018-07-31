using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace MockupToXaml.Model
{
    [XmlRoot(ElementName = "mockup")]
    public class Mockup : ModelBase
    {
        private int _Width;
        [XmlAttribute(AttributeName = "mockupW")]
        public int Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                SafeNotify("Width");
            }
        }

        private int _Height;
        [XmlAttribute(AttributeName = "mockupH")]
        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                SafeNotify("Height");
            }
        }

       
        private List<MockupControl> _Controls;
        [XmlArray(ElementName = "controls")]
        public List<MockupControl> Controls
        {
            get { return _Controls; }
            set
            {
                _Controls = value;
                SafeNotify("Controls");
            }
        }

        public static Mockup LoadFromXML(string filePath)
        {
            // TODO: validation, error checking.

            XmlSerializer xer = new XmlSerializer(typeof(Mockup));
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Mockup mockup = (Mockup)xer.Deserialize(fs);

            fs.Close();
            fs.Dispose();

            // TODO: Optimize: do not read the file twice.  Load once, place on memory stream for deserilization.
            mockup.extractControlProperties(File.ReadAllText(filePath));

            return mockup;

        }

        public string ToXML()
        {

            XmlSerializer xer = new XmlSerializer(typeof(Mockup));

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    xer.Serialize(ms, this);
                    byte[] buffer = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(buffer, 0, buffer.Length);
                    ms.Close();

                    return System.Text.ASCIIEncoding.ASCII.GetString(buffer);
                }

            }
            catch
            {
            }

            return string.Empty;


        }

        /// <summary>
        /// Extracts the control properties from the raw XML document that represents the mockup (BMML) file.
        /// </summary>
        /// <param name="xmlDocument">Raw XML mockup document (BMML) file.</param>
        internal void extractControlProperties(string rawXmlDocument)
        {
            XElement xdoc = XElement.Parse(rawXmlDocument);

    
            foreach (MockupControl control in this.Controls)
            {
                var controls = xdoc.Descendants("controls");
                var xcontrol = controls.Descendants("control").FirstOrDefault(x => x.Element("ID").Value == control.ControlID.ToString());
                if (xcontrol == null)
                {
                    // TODO: Warn? Log? we should not just ignore this.. controlID should have been found.
                    continue;
                }

                control.ControlProperties = new Dictionary<string, string>();

                foreach (XElement xcontrolProp in xcontrol.Elements("properties").Elements())
                {
                    //For now - only handle "Simple" properties
                    if (xcontrolProp.HasElements)
                    {
                        //TODO: Implement some logging
                        continue;
                    }

                    if (!string.IsNullOrEmpty( xcontrolProp.Value ) )
                        control.ControlProperties.Add(xcontrolProp.Name.LocalName, Uri.UnescapeDataString( xcontrolProp.Value ));
                }

            }

        }
    }

}
