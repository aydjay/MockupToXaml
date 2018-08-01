using System.Collections.Generic;
using Newtonsoft.Json;

namespace MockupToXaml.Model
{
    public class MockupControl
    {
        [JsonProperty("ID")]
        public int ControlId { get; set; }

        [JsonProperty("typeID")]
        public string ControlTypeId { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        /// <summary>
        ///     Computed width.  Takes into account possible -1 value for width.
        /// </summary>
        [JsonProperty("measuredW")]
        public int Width { get; set; }

        /// <summary>
        ///     Computed Height.  Takes into account possible -1 value for Height.
        /// </summary>
        [JsonProperty("measuredH")]
        public int Height { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, dynamic> ControlProperties { get; set; }
    }
}