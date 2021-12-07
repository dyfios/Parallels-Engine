namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityPosition
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityPosition
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("position", Required = Required.Always)]
        public PropertiesPosition Position { get; set; }

        [JsonProperty("local", Required = Required.Always)]
        public bool Local { get; set; }
    }

    public partial class PropertiesPosition
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }
}