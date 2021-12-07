using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Utilities;

namespace FiveSQD.Parallels.Runtime.Engine.Scene
{
    public class SceneManager : BaseManager
    {
        public GameObject scenePrefab;

        public GameObject sceneContainer;

        public uint maxScenes = 8;

        private Dictionary<uint, Scene> scenes;

        public override void Initialize()
        {
            base.Initialize();
            scenes = new Dictionary<uint, Scene>();
        }

        public Scene CreateScene()
        {
            if (scenes.Count >= maxScenes)
            {
                LogSystem.LogError("[SceneManager->CreateScene] Only " + maxScenes + " scenes allowed.");
                return null;
            }

            GameObject newSceneObject = Instantiate(scenePrefab);
            Scene sceneScript = newSceneObject.GetComponent<Scene>();
            if (sceneScript == null)
            {
                LogSystem.LogError("[SceneManager->CreateScene] No Scene Script.");
                return null;
            }

            scenes.Add((uint) scenes.Count, sceneScript);
            return sceneScript;
        }

        public void LoadScene(Scene scene, string uri)
        {
            ClearScene(scene);

            uint sceneID = maxScenes;
            foreach (KeyValuePair<uint, Scene> entry in scenes)
            {
                if (entry.Value == scene)
                {
                    sceneID = entry.Key;
                    break;
                }
            }
            if (sceneID == maxScenes)
            {
                LogSystem.LogError("[SceneManager->LoadScene] Scene dictionary error.");
                return;
            }

            Action<Scene.SceneInfo> OnComplete = (Scene.SceneInfo sceneInfo) => {
                SetScene(sceneID, sceneInfo);
            };
            Parallels.VEMLManager.StartVEMLRequest(uri, OnComplete);
        }

        public void SetScene(uint sceneID, Scene.SceneInfo sceneInfo)
        {
            Scene scene = scenes[sceneID];
            if (scene == null)
            {
                LogSystem.LogError("[SceneManager->SetScene] Invalid Scene ID.");
                return;
            }
            scene.SetScene(sceneInfo);
        }

        public void ClearScene(Scene scene)
        {
            scene.Clear();
        }

        public void DestroyScene(Scene scene)
        {
            Destroy(scene);
        }
    }
}