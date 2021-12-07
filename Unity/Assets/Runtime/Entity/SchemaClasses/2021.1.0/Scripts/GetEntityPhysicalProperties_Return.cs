namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPhysicalProperties_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityPhysicalProperties_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("angular-drag", Required = Required.Always)]
        public double AngularDrag { get; set; }

        [JsonProperty("center-of-mass", Required = Required.Always)]
        public PropertiesCenterOfMass CenterOfMass { get; set; }

        [JsonProperty("drag", Required = Required.Always)]
        public double Drag { get; set; }

        [JsonProperty("gravitational", Required = Required.Always)]
        public bool Gravitational { get; set; }

        [JsonProperty("mass", Required = Required.Always)]
        public double Mass { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }

    public partial class PropertiesCenterOfMass
    {
        [JsonProperty("x", Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty("z", Required = Required.Always)]
        public double Z { get; set; }
    }
}