namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityChildren
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityChildren
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }
    }
}