﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MockupToXaml.Model
{
    [XmlType(TypeName="control")]
    public class MockupControl : ModelBase
    {

        private int _ControlID;
        [XmlElement(ElementName = "ID")]
        public int ControlID
        {
            get { return _ControlID; }
            set
            {
                _ControlID = value;
            }
        }

        private string _ControlTypeID;
        [XmlElement(ElementName =  "typeID")]
        public string ControlTypeID
        {
            get { return _ControlTypeID; }
            set
            {
                _ControlTypeID = value;
            }
        }

        private int _X;
        [XmlElement(ElementName =  "x")]
        public int X
        {
            get { return _X; }
            set
            {
                _X = value;
            }
        }

        private int _Y;
        [XmlElement(ElementName =  "y")]
        public int Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
            }
        }

        private int _Width;
        /// <summary>
        /// Computed width.  Takes into account possible -1 value for width.
        /// </summary>
        [XmlElement(ElementName =  "measuredW")]
        public int Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
            }
        }

        private int _Height;
        /// <summary>
        /// Computed Height.  Takes into account possible -1 value for Height.
        /// </summary>
        [XmlElement(ElementName = "measuredH")]
        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
            }
        }

        private Dictionary<string, string> _ControlProperties;
        [XmlIgnore]
        public Dictionary<string, string> ControlProperties
        {
            get { return _ControlProperties; }
            set
            {
                _ControlProperties = value;
            }
        }

    }
}
