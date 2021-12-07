namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPostion_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityPosition_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("position", Required = Required.Always)]
        public PropertiesPosition Position { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
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