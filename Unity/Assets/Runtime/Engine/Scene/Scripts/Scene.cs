using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Utilities;
using FiveSQD.Parallels.Entity;

namespace FiveSQD.Parallels.Runtime.Engine.Scene
{
    public class Scene : MonoBehaviour
    {
        public class SceneInfo
        {
            public string title;

            public string[] scripts;

            public BaseEntity[] entities;

            public SceneInfo(string sceneTitle, string[] sceneScripts, BaseEntity[] sceneEntities)
            {
                title = sceneTitle;
                scripts = sceneScripts;
                entities = sceneEntities;
            }
        }

        public GameObject entityContainer;

        private TabController connectedTab;

        public void SetVisible()
        {

        }

        public void SetStopped()
        {

        }

        public void Initialize(TabController tab)
        {
            connectedTab = tab;
        }

        public void SetScene(SceneInfo sceneInfo)
        {
            Clear();

            SetTitle(sceneInfo.title);
            foreach (string script in sceneInfo.scripts)
            {
                RunScript(script);
            }
            foreach (BaseEntity entity in sceneInfo.entities)
            {
                AddEntity(entity);
            }
        }

        public void SetTitle(string title)
        {
            if (connectedTab == null)
            {
                LogSystem.LogError("[Scene->SetTitle] No connected tab.");
                return;
            }

            connectedTab.SetName(title);
        }

        public string GetTitle()
        {
            if (connectedTab == null)
            {
                LogSystem.LogError("[Scene->GetTitle] No connected tab.");
                return null;
            }

            return connectedTab.GetName();
        }

        public void RunScript(string script)
        {
            Parallels.JavascriptManager.RunScript(script);
        }

        public void AddEntity(BaseEntity entity)
        {
            if (entity == null)
            {
                LogSystem.LogError("[Scene->AddEntity] Null entity.");
                return;
            }

            entity.transform.SetParent(entityContainer.transform);
        }

        public bool IsEntityInScene(BaseEntity entity)
        {
            return entity.transform.IsChildOf(entityContainer.transform);
        }

        public void Clear()
        {

        }
    }
}