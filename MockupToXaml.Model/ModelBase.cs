using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Data;
using System.Xml.Serialization;

namespace MockupToXaml.Model
{
    public class ModelBase
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("-=-=] {0} [=-=-\r\n", this.GetType());

            foreach (System.Reflection.PropertyInfo pi in this.GetType().GetProperties())
            {
                if (pi.CanRead)
                {
                    try
                    {
                        sb.AppendFormat("] {0} = '{1}', [{2}]\r\n",
                            pi.Name, pi.GetValue(this, null), pi.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        sb.AppendFormat("- {0} threw error: {1}",
                            pi.Name, ex.Message);
                        // just ignore, should never get an error reading a property but you never know.
                    }
                }
            }

            sb.AppendLine("-=-=-=-=-=-=-=-=-=-=-=-=-");

            return sb.ToString();
        }
    }
}
