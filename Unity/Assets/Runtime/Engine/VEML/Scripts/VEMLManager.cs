using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using FiveSQD.Libraries.VEML;
using FiveSQD.Parallels.HTTP;
using FiveSQD.Utilities;

namespace FiveSQD.Parallels.Runtime.Engine.VEML
{
    public class VEMLManager : BaseManager
    {
        public float timeout = 10;

        public override void Initialize()
        {
            base.Initialize();
        }

        public IEnumerator StartVEMLRequest(string uri, Action<Scene.Scene.SceneInfo> onFinished)
        {
            Action<long, byte[]> onResult = (long responseCode, byte[] data) => {
                StartCoroutine(OnVEMLResult(uri, responseCode, data, onFinished));
            };
            yield return HTTPHelper.GetRequest(uri, onResult);
        }

        public IEnumerator StartScriptRequest(string uri,
            Guid scriptID, Dictionary<Guid, string> scriptDictionary)
        {
            Action<long, byte[]> onResult = (long responseCode, byte[] data) => {
                StartCoroutine(OnScriptResult(scriptID, scriptDictionary, responseCode, data));
            };
            yield return HTTPHelper.GetRequest(uri, onResult);
        }

        public IEnumerator StartFileRequest(string uri, Dictionary<string, bool> fileDictionary)
        {
            Action<long, byte[]> onResult = (long responseCode, byte[] data) => {
                StartCoroutine(OnFileResult(uri, fileDictionary, responseCode, data));
            };
            yield return HTTPHelper.GetRequest(uri, onResult);
        }

        private IEnumerator OnVEMLResult(string uri, long responseCode, byte[] data,
            Action<Scene.Scene.SceneInfo> onFinished)
        {
            LogSystem.Log("Got response " + responseCode + " for request " + uri);

            if (responseCode != 200)
            {
                LogSystem.Log("Error loading document.");
                yield break;
            }

            veml veml = null;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(veml));
                MemoryStream mem = new MemoryStream(data);
                veml = (veml) ser.Deserialize(mem);
            }
            catch (Exception e)
            {
                LogSystem.LogWarning("VEML document deserialization failure " + e.ToString());
            }

            if (veml == null)
            {
                LogSystem.Log("Not a VEML document: " + System.Text.Encoding.UTF8.GetString(data));
            }
            else
            {
                yield return LoadVEMLDocument(veml, Path.GetDirectoryName(uri), onFinished);
            }
        }

        private IEnumerator OnScriptResult(Guid scriptID,
            Dictionary<Guid, string> scriptDictionary, long responseCode, byte[] data)
        {
            LogSystem.Log("Got script response " + responseCode + " for request " + scriptID.ToString());

            if (responseCode != 200)
            {
                LogSystem.Log("Error loading script.");
                yield break;
            }

            scriptDictionary[scriptID] = System.Text.Encoding.UTF8.GetString(data);
        }

        private IEnumerator OnFileResult(string filePath,
            Dictionary<string, bool> fileDictionary, long responseCode, byte[] data)
        {
            LogSystem.Log("Got file response " + responseCode + " for request " + filePath);

            if (responseCode != 200)
            {
                LogSystem.Log("Error loading file.");
                yield break;
            }

            StoreFile(filePath, data);
            fileDictionary[filePath] = true;
        }

        private IEnumerator LoadVEMLDocument(veml document, string uri,
            Action<Scene.Scene.SceneInfo> onFinished)
        {
            Dictionary<Guid, string> scriptsToRun = new Dictionary<Guid, string>();
            Dictionary<string, bool> entityFiles = new Dictionary<string, bool>();
            Dictionary<entity, entity> entities;
            Dictionary<entity, Entity.BaseEntity> entityDictionary = new Dictionary<entity, Entity.BaseEntity>();
            List<Entity.BaseEntity> loadedEntities = new List<Entity.BaseEntity>();
            string fileURI = uri.Replace(":", "~");

            // Validate metadata.
            if (document.metadata == null)
            {
                LogSystem.LogWarning("VEML document missing required field: metadata.");
                yield break;
            }
            else
            {
                if (string.IsNullOrEmpty(document.metadata.title))
                {
                    LogSystem.LogWarning("VEML document metadata missing required field: title.");
                    yield break;
                }
                else
                {

                }

                // Set up scripts.
                if (document.metadata.script != null)
                {
                    foreach (string script in document.metadata.script)
                    {
                        Guid scriptID = Guid.NewGuid();
                        if (Uri.IsWellFormedUriString(uri + "/" + script, UriKind.RelativeOrAbsolute))
                        {
                            scriptsToRun.Add(scriptID, null);
                            yield return StartCoroutine(StartScriptRequest(uri + "/" + script, scriptID, scriptsToRun));
                        }
                        else
                        {
                            scriptsToRun.Add(scriptID, script);
                        }
                    }
                }
            }

            // Validate environment.
            if (document.environment == null)
            {
                LogSystem.LogWarning("VEML document missing required field: environment.");
                yield break;
            }
            else
            {
                entities = GetAllChildEntities(document.environment);

                foreach (entity entity in entities.Keys)
                {
                    if (entity.tag == null)
                    {
                        LogSystem.LogWarning("VEML document environment entity missing required field: tag.");
                        yield break;
                    }
                    else
                    {

                    }

                    if (entity.transform == null)
                    {
                        LogSystem.LogWarning("VEML document environment entity missing required field: transform.");
                        yield break;
                    }
                    else
                    {
                        if (entity.transform.position == null)
                        {
                            LogSystem.LogWarning("VEML document environment entity transform missing required field: position.");
                            yield break;
                        }
                        else
                        {

                        }

                        if (entity.transform.rotation == null)
                        {
                            LogSystem.LogWarning("VEML document environment entity transform missing required field: rotation.");
                            yield break;
                        }
                        else
                        {

                        }

                        if (entity.transform.Item == null)
                        {
                            LogSystem.LogWarning("VEML document environment entity transform missing required field: scale or size.");
                            yield break;
                        }
                        else
                        {
                            if (entity.transform.Item is scale)
                            {

                            }
                            else if (entity.transform.Item is size)
                            {

                            }
                            else
                            {
                                LogSystem.LogWarning("VEML document environment entity transform does not contain size/scale.");
                                yield break;
                            }
                        }
                    }

                    // Handle entity specific properties.
                    if (entity is meshentity)
                    {
                        if (((meshentity) entity).mesh_resources == null)
                        {
                            LogSystem.LogWarning("VEML document environment mesh entity missing required field: mesh_resources.");
                            yield break;
                        }
                        
                        foreach (string resource in ((meshentity) entity).mesh_resources)
                        {
                            entityFiles.Add(uri + "/" + resource, false);
                            yield return StartCoroutine(StartFileRequest(uri + "/" + resource, entityFiles));
                        }
                    }
                    else if (entity is containerentity)
                    {

                    }
                    else
                    {
                        LogSystem.LogWarning("Unknown kind of entity: " + entity.tag);
                    }
                }
            }

            // Wait for all scripts to download.
            float elapsedTime = 0f;
            bool allLoaded = true;
            do
            {
                allLoaded = true;
                foreach (string script in scriptsToRun.Values)
                {
                    if (script == null)
                    {
                        allLoaded = false;
                        yield return new WaitForSeconds(0.25f);
                        elapsedTime += 0.25f;
                        break;
                    }
                }
                foreach (bool file in entityFiles.Values)
                {
                    if (file == false)
                    {
                        allLoaded = false;
                        yield return new WaitForSeconds(0.25f);
                        elapsedTime += 0.25f;
                        break;
                    }
                }
            } while (allLoaded == false && elapsedTime < timeout);

            List<string> scripts = new List<string>();
            foreach (string script in scriptsToRun.Values)
            {
                scripts.Add(script);
            }

            foreach (KeyValuePair<entity, entity> entity in entities)
            {
                string parentID = null;
                if (entity.Value != null)
                {
                    Entity.BaseEntity parentEntity = entityDictionary[entity.Key];
                    if (parentEntity != null)
                    {
                        parentID = parentEntity.id.ToString();
                    }
                }
                bool isSize = false;
                Vector3 scaleField;
                if (entity.Key.transform.Item is scale)
                {
                    scaleField = ((scale) entity.Key.transform.Item).ToVector3();
                }
                else
                {
                    scaleField = ((size) entity.Key.transform.Item).ToVector3();
                    isSize = true;
                }
                Entity.BaseEntity newEntity = null;
                if (entity.Key is meshentity)
                {
                    string entityIDString = Entity.EntityAPI.LoadMeshEntity(
                        ((meshentity) entity.Key).mesh_name,
                        parentID, entity.Key.transform.position.ToVector3(),
                        entity.Key.transform.rotation.ToQuaternion(), scaleField, isSize);
                    if (string.IsNullOrEmpty(entityIDString))
                    {
                        LogSystem.LogWarning("[VEMLManager->LoadVEMLDocument] Error loading mesh entity.");
                        continue;
                    }

                    elapsedTime = 0;
                    while (Entity.EntityManager.Exists(Guid.Parse(entityIDString)) == false && elapsedTime < timeout)
                    {
                        elapsedTime += 0.25f;
                        yield return new WaitForSeconds(0.25f);
                    }

                    if (Entity.EntityManager.Exists(Guid.Parse(entityIDString)) == false)
                    {
                        LogSystem.LogWarning("[VEMLManager->LoadVEMLDocument] Entity load timed out.");
                        yield break;
                    }

                    newEntity = Entity.EntityManager.FindEntity(Guid.Parse(entityIDString));
                    if (newEntity == null)
                    {
                        LogSystem.LogWarning("[VEMLManager->LoadVEMLDocument] Error finding loaded mesh entity.");
                    }
                }
                else if (entity.Key is containerentity)
                {
                    newEntity = Entity.EntityManager.FindEntity(Guid.Parse(Entity.EntityAPI.LoadContainerEntity(
                        parentID, entity.Key.transform.position.ToVector3(),
                        entity.Key.transform.rotation.ToQuaternion(), scaleField, isSize)));
                }
                else
                {

                }
                loadedEntities.Add(newEntity);
            }

            onFinished.Invoke(new Scene.Scene.SceneInfo(
                document.metadata.title, scripts.ToArray(), loadedEntities.ToArray()));
        }

        private Dictionary<entity, entity> GetAllChildEntities(entity[] entities)
        {
            Dictionary<entity, entity> foundEntities = new Dictionary<entity, entity>();
            foreach (entity entity in entities)
            {
                Dictionary<entity, entity> newEntities = GetAllChildEntities(entity);
                foreach (KeyValuePair<entity, entity> newEntity in newEntities)
                {
                    foundEntities.Add(newEntity.Key, newEntity.Value);
                }
            }
            return foundEntities;
        }

        private Dictionary<entity, entity> GetAllChildEntities(entity entity)
        {
            Dictionary<entity, entity> entities = new Dictionary<entity, entity>();

            Queue<KeyValuePair<entity, entity>> entityQueue = new Queue<KeyValuePair<entity, entity>>();
            entityQueue.Enqueue(new KeyValuePair<entity, entity>(entity, null));
            while (entityQueue.Count > 0)
            {
                KeyValuePair<entity, entity> item = entityQueue.Dequeue();
                if (entity.children != null)
                {
                    foreach (entity child in entity.children)
                    {
                        entityQueue.Enqueue(new KeyValuePair<entity, entity>(child, entity));
                    }
                }
                entities.Add(item.Key, item.Value);
            }

            return entities;
        }

        private void StoreFile(string name, byte[] data)
        {
            string filePath = name.Replace(":", "~");
            if (Parallels.IOManager.FileExistsInCacheDirectory(filePath))
            {
                Parallels.IOManager.DeleteFileInCacheDirectory(filePath);
                Parallels.IOManager.CreateFileInCacheDirectory(filePath, data);
            }
            else
            {
                Parallels.IOManager.CreateFileInCacheDirectory(filePath, data);
            }
        }
    }
}