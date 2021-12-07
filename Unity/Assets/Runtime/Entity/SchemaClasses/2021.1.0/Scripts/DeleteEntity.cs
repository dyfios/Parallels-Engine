namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.DeleteEntity
{
    using System;

    using Newtonsoft.Json;

    public partial class DeleteEntity
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }
    }
}