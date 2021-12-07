namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityParent
{
    using System;

    using Newtonsoft.Json;

    public partial class SetEntityParent
    {
        [JsonProperty("entity-uuid", Required = Required.Always)]
        public Guid EntityUuid { get; set; }

        [JsonProperty("parent-uuid", Required = Required.Always)]
        public Guid ParentUuid { get; set; }
    }
}