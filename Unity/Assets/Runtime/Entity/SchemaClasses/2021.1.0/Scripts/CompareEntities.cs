namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.CompareEntities
{
    using System;
    
    using Newtonsoft.Json;

    public partial class CompareEntities
    {
        [JsonProperty("entity-1-uuid", Required = Required.Always)]
        public Guid Entity1Uuid { get; set; }

        [JsonProperty("entity-2-uuid", Required = Required.Always)]
        public Guid Entity2Uuid { get; set; }
    }
}