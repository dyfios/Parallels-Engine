namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntitySize_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntitySize_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("size", Required = Required.Always)]
        public PropertiesSize Size { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }

    public partial class PropertiesSize
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }
}