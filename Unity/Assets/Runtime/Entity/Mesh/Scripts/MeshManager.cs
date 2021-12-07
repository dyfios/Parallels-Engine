using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Parallels.Runtime.Engine.Tags;
using FiveSQD.Parallels.Runtime.Engine;

namespace FiveSQD.Parallels.Infrastructure.Models
{
    public class MeshManager : BaseManager
    {
        private static readonly Vector3 prefabLocation = new Vector3(9999, 9999, 9999);

        private Dictionary<string, GameObject> loadedGLTFMeshPrefabs = new Dictionary<string, GameObject>();
        private static MeshManager instance;

        public override void Initialize()
        {
            base.Initialize();

            instance = this;
        }

        public static void LoadMeshFromGLTF(string path, Action<GameObject> onLoaded)
        {
            if (instance.loadedGLTFMeshPrefabs.ContainsKey(path))
            {
                instance.InstantiateMeshFromPrefab(instance.loadedGLTFMeshPrefabs[path], onLoaded);
            }
            else
            {
                Action<GameObject, AnimationClip[]> callback =
                    (GameObject go, AnimationClip[] ac) => { instance.MeshLoadedFromGLTF(path, go, ac, onLoaded); };
                GLTFLoader.LoadModelAsync(path, callback);
            }
        }

        public void MeshLoadedFromGLTF(string path, GameObject result, AnimationClip[] clips, Action<GameObject> callback)
        {
            loadedGLTFMeshPrefabs.Add(path, result);
            result.transform.position = prefabLocation;
            SetUpMeshPrefab(result);
            InstantiateMeshFromPrefab(result, callback);
        }

        private void InstantiateMeshFromPrefab(GameObject prefab, Action<GameObject> callback)
        {
            GameObject loadedMesh = Instantiate(prefab);
            loadedMesh.SetActive(true);
            callback.Invoke(loadedMesh);
        }

        private void SetUpMeshPrefab(GameObject prefab)
        {
            prefab.SetActive(false);
            Debug.Log(prefab.name);
            MeshFilter[] meshFilters = prefab.GetComponentsInChildren<MeshFilter>();
            List<Mesh> meshes = new List<Mesh>();
            if (meshFilters.Length > 0)
            {
                foreach (MeshFilter meshFilter in meshFilters)
                {
                    meshes.Add(meshFilter.mesh);

                    GameObject mcGO = new GameObject("MeshCollider");
                    mcGO.transform.SetParent(meshFilter.transform);
                    mcGO.transform.localPosition = Vector3.zero;
                    mcGO.transform.localRotation = Quaternion.identity;
                    mcGO.transform.localScale = Vector3.one;
                    MeshCollider meshCollider = mcGO.AddComponent<MeshCollider>();
                    meshCollider.sharedMesh = meshFilter.mesh;
                    meshCollider.tag = TagManager.meshColliderTag;
                }
            }
            else
            {
                Utilities.LogSystem.LogWarning("[MeshManager->SetUpMeshPrefab] Unable to set up mesh.");
                return;
            }

            Bounds bounds = new Bounds(prefab.transform.position, Vector3.zero);
            MeshRenderer[] rends = prefab.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in rends)
            {
                bounds.Encapsulate(rend.bounds);
            }

            bounds.center = bounds.center - prefab.transform.position;

            GameObject bcGO = new GameObject("BoxCollider");
            bcGO.transform.SetParent(prefab.transform);
            bcGO.transform.localPosition = Vector3.zero;
            BoxCollider boxCollider = bcGO.AddComponent<BoxCollider>();
            boxCollider.tag = TagManager.physicsColliderTag;
            boxCollider.center = bounds.center;
            boxCollider.size = bounds.size;

            Rigidbody rigidbody = prefab.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
        }
    }
}