namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityMotion_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityMotion_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("angular-velocity", Required = Required.Always)]
        public PropertiesAngularVelocity AngularVelocity { get; set; }

        [JsonProperty("stationary", Required = Required.Always)]
        public bool Stationary { get; set; }

        [JsonProperty("velocity", Required = Required.Always)]
        public PropertiesVelocity Velocity { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }

    public partial class PropertiesAngularVelocity
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }

    public partial class PropertiesVelocity
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }
}