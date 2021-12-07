namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.DeleteEntity_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class DeleteEntity_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }
}