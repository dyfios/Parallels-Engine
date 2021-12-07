namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPosition
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityPosition
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("local", Required = Required.Always)]
        public bool Local { get; set; }
    }
}