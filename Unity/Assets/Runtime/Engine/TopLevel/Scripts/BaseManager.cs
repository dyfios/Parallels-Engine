using UnityEngine;

namespace FiveSQD.Parallels.Runtime.Engine
{
    public class BaseManager : MonoBehaviour
    {
        public virtual void Initialize()
        {
            Utilities.LogSystem.Log("[" + GetType().Name + "] Initialized.");
        }
    }
}