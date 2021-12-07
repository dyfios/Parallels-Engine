namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntitySize
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntitySize
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("size", Required = Required.Always)]
        public PropertiesSize Size { get; set; }
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