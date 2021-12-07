namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityMotion
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityMotion
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("angular-velocity", Required = Required.Always)]
        public PropertiesAngularVelocity AngularVelocity { get; set; }

        [JsonProperty("velocity", Required = Required.Always)]
        public PropertiesVelocity Velocity { get; set; }

        [JsonProperty("stationary", Required = Required.Always)]
        public bool Stationary { get; set; }
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

    public partial class PropertiesAngularVelocity
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }
}