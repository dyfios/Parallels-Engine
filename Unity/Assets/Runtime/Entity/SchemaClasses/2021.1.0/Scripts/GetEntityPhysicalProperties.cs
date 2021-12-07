namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPhysicalProperties
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityPhysicalProperties
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }
    }
}