namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityRotation
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityRotation
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("local", Required = Required.Always)]
        public bool Local { get; set; }
    }
}