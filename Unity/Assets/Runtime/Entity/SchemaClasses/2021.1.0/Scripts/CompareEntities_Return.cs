namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.CompareEntities_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class CompareEntities_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("equal", Required = Required.Always)]
        public bool Equal { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }
}