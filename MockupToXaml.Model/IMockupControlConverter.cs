using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MockupToXaml.Model
{
    public interface IMockupControlConverter
    {
        
        string ConvertMockupToXaml(MockupControl control);

        MockupTemplate Template { get; set; }
    }
}
