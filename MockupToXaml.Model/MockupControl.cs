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
        [XmlAttribute(AttributeName = "controlID")]
        public int ControlID
        {
            get { return _ControlID; }
            set
            {
                _ControlID = value;
                SafeNotify("ControlID");
            }
        }

        private string _ControlTypeID;
        [XmlAttribute(AttributeName = "controlTypeID")]
        public string ControlTypeID
        {
            get { return _ControlTypeID; }
            set
            {
                _ControlTypeID = value;
                SafeNotify("ControlTypeID");
            }
        }

        private int _X;
        [XmlAttribute(AttributeName = "x")]
        public int X
        {
            get { return _X; }
            set
            {
                _X = value;
                SafeNotify("X");
            }
        }

        private int _Y;
        [XmlAttribute(AttributeName = "y")]
        public int Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                SafeNotify("Y");
            }
        }

        private int _Width;
        /// <summary>
        /// Computed width.  Takes into account possible -1 value for width.
        /// </summary>
        [XmlAttribute(AttributeName = "w")]
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
        /// <summary>
        /// Computed Height.  Takes into account possible -1 value for Height.
        /// </summary>
        [XmlAttribute(AttributeName = "h")]
        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                SafeNotify("Height");
            }
        }

        private int _MeasuredWidth;
        /// <summary>
        /// Actual measuredW value from the BMML.
        /// </summary>
        [XmlAttribute(AttributeName = "measuredW")]
        public int MeasuredWidth
        {
            get { return _MeasuredWidth; }
            set
            {
                _MeasuredWidth = value;
                SafeNotify("MeasuredWidth");
            }
        }

        private int _MeasuredHeight;
        /// <summary>
        /// Actual measuredH value from the BMML.
        /// </summary>
        [XmlAttribute(AttributeName = "measuredH")]
        public int MeasuredHeight
        {
            get { return _MeasuredHeight; }
            set
            {
                _MeasuredHeight = value;
                SafeNotify("MeasuredHeight");
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
                SafeNotify("ControlProperties");
            }
        }

    }
}
