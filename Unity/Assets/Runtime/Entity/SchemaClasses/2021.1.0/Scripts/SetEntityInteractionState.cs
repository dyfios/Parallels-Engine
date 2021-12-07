namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityInteractionState
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityInteractionState
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("interaction-state", Required = Required.Always)]
        public string InteractionState { get; set; }
    }
}