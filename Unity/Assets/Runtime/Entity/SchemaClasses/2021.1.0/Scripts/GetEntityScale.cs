namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityScale
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityScale
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }
    }
}