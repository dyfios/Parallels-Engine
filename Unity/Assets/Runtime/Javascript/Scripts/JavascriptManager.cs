using FiveSQD.Utilities;
using FiveSQD.Parallels.Runtime.Engine;

namespace FiveSQD.Parallels.Runtime.Javascript
{
    public class JavascriptManager : BaseManager
    {
        private static readonly System.Tuple<string, System.Type>[] apis = new System.Tuple<string, System.Type>[]
        {
            new System.Tuple<string, System.Type>("Logging", typeof(LogSystem)),
            new System.Tuple<string, System.Type>("Vector3", typeof(UnityEngine.Vector3)),
            new System.Tuple<string, System.Type>("Quaternion", typeof(UnityEngine.Quaternion)),
            new System.Tuple<string, System.Type>("Entity", typeof(Entity.EntityAPI))
        };

        private Jint.Engine jsEngine;

        public override void Initialize()
        {
            jsEngine = new Jint.Engine();
            RegisterAllAPIs();
        }

        public void RegisterAllAPIs()
        {
            if (jsEngine == null)
            {
                LogSystem.LogError("[JavascriptManager->RegisterAllAPIs] No jsEngine reference.");
                return;
            }

            foreach (System.Tuple<string, System.Type> api in apis)
            {
                RegisterAPI(api.Item1, api.Item2);
            }
        }

        public void RegisterAPI(string name, System.Type type)
        {
            if (jsEngine == null)
            {
                LogSystem.LogError("[JavascriptManager->RegisterAPI] No jsEngine reference.");
                return;
            }

            jsEngine.SetValue(name, type);
        }

        public void RunScript(string script)
        {
            if (jsEngine == null)
            {
                LogSystem.LogError("[JavascriptManager->RunScript] No jsEngine reference.");
                return;
            }

            jsEngine.Execute(script);
        }

        public object Run(string logic)
        {
            if (jsEngine == null)
            {
                LogSystem.LogError("[JavascriptManager->Run] No jsEngine reference.");
                return null;
            }

            return jsEngine.Evaluate(logic);
        }
    }
}