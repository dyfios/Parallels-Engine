namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityRotation
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityRotation
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("rotation", Required = Required.Always)]
        public PropertiesRotation Rotation { get; set; }

        [JsonProperty("local", Required = Required.Always)]
        public bool Local { get; set; }
    }

    public partial class PropertiesRotation
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }

        [JsonProperty("w", Required = Required.Always)]
        public double W { get; set; }
    }
}