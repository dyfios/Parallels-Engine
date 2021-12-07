using System;
using UnityEngine;
using Siccity.GLTFUtility;

namespace FiveSQD.Parallels.Infrastructure.Models
{
    /// <summary>
    /// Component to load a GLTF scene with
    /// </summary>
    public class GLTFLoader : MonoBehaviour
    {
        private static GLTFLoader instance;

        private void Start()
        {
            instance = this;
        }

        public static void LoadModelAsync(string pathToModel, Action<GameObject, AnimationClip[]> callback)
        {
            Importer.ImportGLTFAsync(pathToModel, new ImportSettings(), callback);
        }

        public static GameObject LoadModel(string pathToModel)
        {
            return Importer.LoadFromFile(pathToModel);
        }
    }
}