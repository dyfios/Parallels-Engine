namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityChildren_Return
{
    using System;

    using Newtonsoft.Json;

    public partial class GetEntityChildren_Return
    {
        [JsonProperty("request-uuid", Required = Required.Always)]
        public Guid RequestUuid { get; set; }

        [JsonProperty("child-uuids", Required = Required.Always)]
        public Guid[] ChildUuids { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }
    }
}