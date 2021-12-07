using UnityEngine;
using FiveSQD.Parallels.Runtime.Engine.Layers;
using FiveSQD.Parallels.Runtime.Engine.Tags;
using FiveSQD.Parallels.Runtime.Engine.Materials;
using FiveSQD.Parallels.Runtime.Engine.VOSRouting;
using FiveSQD.Parallels.Entity;
using FiveSQD.Parallels.Daemon;
using FiveSQD.Parallels.Runtime.Engine.Scene;
using FiveSQD.Parallels.Runtime.Engine.VEML;
using FiveSQD.Parallels.Runtime.Javascript;
using FiveSQD.Parallels.Runtime.IO;

namespace FiveSQD.Parallels.Runtime
{
    public class Parallels : MonoBehaviour
    {
        public const string VERSION = "0.1.0";

        public string vosServerKey;

        public string vosServerHost;

        public uint vosServerPort;

        public uint vosServerConnectAttempts = 8;

        public float vosServerConnectInterval;
        
        public static LayerManager LayerManager
        {
            get
            {
                return instance.layerManager;
            }
        }

        public static TagManager TagManager
        {
            get
            {
                return instance.tagManager;
            }
        }

        public static MaterialManager MaterialManager
        {
            get
            {
                return instance.materialManager;
            }
        }

        public static VOSManager VOSManager
        {
            get
            {
                return instance.vosManager;
            }
        }

        public static EntityManager EntityManager
        {
            get
            {
                return instance.entityManager;
            }
        }

        public static DaemonManager DaemonManager
        {
            get
            {
                return instance.daemonManager;
            }
        }

        public static SceneManager SceneManager
        {
            get
            {
                return instance.sceneManager;
            }
        }

        public static VEMLManager VEMLManager
        {
            get
            {
                return instance.vemlManager;
            }
        }

        public static JavascriptManager JavascriptManager
        {
            get
            {
                return instance.javascriptManager;
            }
        }

        public static IOManager IOManager
        {
            get
            {
                return instance.ioManager;
            }
        }

        public static TabsController TabsController
        {
            get
            {
                return instance.tabsController;
            }
        }

        private static Parallels instance;

        public LayerManager layerManager;

        public TagManager tagManager;

        public MaterialManager materialManager;

        public VOSManager vosManager;

        public EntityManager entityManager;

        public DaemonManager daemonManager;

        public SceneManager sceneManager;

        public VEMLManager vemlManager;

        public JavascriptManager javascriptManager;

        public IOManager ioManager;

        public TabsController tabsController;

        private void Awake()
        {
            instance = this;

            InitializeManagers();
        }

        private void InitializeManagers()
        {
            Utilities.LogSystem.Log("[Parallels] Initializing layer manager...");
            if (layerManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate layer manager. Aborting...");
                Application.Quit();
            }
            layerManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Layer manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing tag manager...");
            if (tagManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate tag manager. Aborting...");
                Application.Quit();
            }
            tagManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Tag manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing material manager...");
            if (materialManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate material manager. Aborting...");
                Application.Quit();
            }
            materialManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Material manager initialized.");

            //Utilities.LogSystem.Log("[Parallels] Initializing VOS manager...");
            //if (vosManager == null)
            //{
            //    Utilities.LogSystem.LogError("[Parallels] Unable to locate VOS manager. Aborting...");
            //    Application.Quit();
            //}
            //vosManager.Initialize();
            //Utilities.LogSystem.Log("[Parallels] VOS manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing Entity manager...");
            if (entityManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate Entity manager. Aborting...");
                Application.Quit();
            }
            entityManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Entity manager initialized.");

            //Utilities.LogSystem.Log("[Parallels] Initializing Daemon manager...");
            //if (daemonManager == null)
            //{
            //    Utilities.LogSystem.LogError("[Parallels] Unable to locate Daemon manager. Aborting...");
            //    Application.Quit();
            //}
            //daemonManager.Initialize(vosServerKey, vosServerHost,
            //    vosServerPort, vosServerConnectAttempts, vosServerConnectInterval);
            //Utilities.LogSystem.Log("[Parallels] Daemon manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing Scene manager...");
            if (sceneManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate Scene manager. Aborting...");
                Application.Quit();
            }
            sceneManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Scene manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing VEML manager...");
            if (vemlManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate VEML manager. Aborting...");
                Application.Quit();
            }
            vemlManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] VEML manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing Javascript manager...");
            if (javascriptManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate Javascript manager. Aborting...");
                Application.Quit();
            }
            javascriptManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] Javascript manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing IO manager...");
            if (ioManager == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate IO manager. Aborting...");
                Application.Quit();
            }
            ioManager.Initialize();
            Utilities.LogSystem.Log("[Parallels] IO manager initialized.");

            Utilities.LogSystem.Log("[Parallels] Initializing Tabs controller...");
            if (tabsController == null)
            {
                Utilities.LogSystem.LogError("[Parallels] Unable to locate Tabs controller. Aborting...");
                Application.Quit();
            }
            tabsController.Initialize();
            Utilities.LogSystem.Log("[Parallels] Tabs controller initialized.");
        }
    }
}