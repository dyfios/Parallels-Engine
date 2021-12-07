namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityHighlightState
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityHighlightState
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("highlight", Required = Required.Always)]
        public bool Highlight { get; set; }
    }
}