namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityParent_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityParent_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("parent-uuid", Required = Required.Always)]
        public Guid ParentUuid { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }
}