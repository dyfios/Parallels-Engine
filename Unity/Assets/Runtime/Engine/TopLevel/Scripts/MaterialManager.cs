using UnityEngine;

namespace FiveSQD.Parallels.Runtime.Engine.Materials
{
    public class MaterialManager : BaseManager
    {
        public static Material HighlightMaterial
        {
            get
            {
                return instance.highlightMaterial;
            }
        }

        public Material highlightMaterial;

        private static MaterialManager instance;

        private void Start()
        {
            instance = this;
        }
    }
}