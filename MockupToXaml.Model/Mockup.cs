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
    }
}