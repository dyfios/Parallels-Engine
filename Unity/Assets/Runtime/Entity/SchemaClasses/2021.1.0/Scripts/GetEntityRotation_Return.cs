namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityRotation_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityRotation_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("rotation", Required = Required.Always)]
        public PropertiesRotation Rotation { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
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