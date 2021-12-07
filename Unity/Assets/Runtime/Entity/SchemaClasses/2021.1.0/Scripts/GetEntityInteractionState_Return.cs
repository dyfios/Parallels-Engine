namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityInteractionState_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityInteractionState_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("interaction-state", Required = Required.Always)]
        public string InteractionState { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }
}