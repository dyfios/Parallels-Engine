// Generated by https://quicktype.io

namespace FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.LoadMeshEntity
{
    using System;
    
    using Newtonsoft.Json;

    public partial class LoadMeshEntity
    {
        [JsonProperty("entity-path", Required = Required.Always)]
        public string EntityPath { get; set; }

        [JsonProperty("parent-uuid", Required = Required.Always)]
        public Guid ParentUuid { get; set; }

        [JsonProperty("position", Required = Required.Always)]
        public PropertiesPosition Position { get; set; }

        [JsonProperty("rotation", Required = Required.Always)]
        public PropertiesRotation Rotation { get; set; }

        [JsonProperty("scale", Required = Required.Always)]
        public PropertiesScale Scale { get; set; }
    }

    public partial class PropertiesPosition
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }

    public partial class PropertiesRotation
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }

        [JsonProperty("w")]
        public double W { get; set; }
    }
    
    public partial class PropertiesScale
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
    }
}