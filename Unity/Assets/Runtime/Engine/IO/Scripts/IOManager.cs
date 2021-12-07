using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FiveSQD.Parallels.Runtime.Engine;
using FiveSQD.Utilities;

namespace FiveSQD.Parallels.Runtime.IO
{
    public class IOManager : BaseManager
    {
        public static string CACHEDIRECTORYPATH { get; private set; }

        private DirectoryInfo cacheDirectory;

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            CACHEDIRECTORYPATH = Path.Combine(Application.dataPath, "cache");
            if (Directory.Exists(CACHEDIRECTORYPATH))
            {
                LogSystem.Log("[IOManager->Initialize] Cache directory exists.");
            }
            else
            {
                LogSystem.Log("[IOManager->Initialize] Cache directory does not exist. Creating...");
                Directory.CreateDirectory(CACHEDIRECTORYPATH);
            }
        }

        public bool FileExistsInCacheDirectory(string file)
        {
            return File.Exists(Path.Combine(CACHEDIRECTORYPATH, file));
        }

        public void CreateFileInCacheDirectory(string fileName, byte[] data)
        {
            if (FileExistsInCacheDirectory(fileName))
            {
                LogSystem.LogWarning("[IOManager->CreateFileInCacheDirectory] File already exists: " + fileName);
                return;
            }

            CreateDirectoryStructure(Path.Combine(CACHEDIRECTORYPATH, fileName));
            File.WriteAllBytes(Path.Combine(CACHEDIRECTORYPATH, fileName), data);
        }

        public void DeleteFileInCacheDirectory(string fileName)
        {
            if (!FileExistsInCacheDirectory(fileName))
            {
                LogSystem.LogWarning("[IOManager->DeleteFileInCacheDirectory] No file: " + fileName);
                return;
            }

            File.Delete(Path.Combine(CACHEDIRECTORYPATH, fileName));
        }

        public byte[] GetFileInCacheDirectory(string fileName)
        {
            if (!FileExistsInCacheDirectory(fileName))
            {
                LogSystem.LogWarning("[IOManager->GetFileInCacheDirectory] No file: " + fileName);
                return null;
            }

            return File.ReadAllBytes(Path.Combine(CACHEDIRECTORYPATH, fileName));
        }

        private void CreateDirectoryStructure(string fileName)
        {
            Debug.Log(Path.GetDirectoryName(fileName));
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
        }
    }
}