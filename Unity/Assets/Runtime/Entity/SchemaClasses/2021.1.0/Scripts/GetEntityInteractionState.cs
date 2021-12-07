namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityInteractionState
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityInteractionState
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }
    }
}