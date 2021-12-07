using System;
using UnityEngine;
using FiveSQD.Libraries.VOSMessenger;

namespace FiveSQD.Parallels.Runtime.Engine.VOSRouting
{
    public class VOSManager : BaseManager
    {
        public VOS vos;

        public VOSMessageRouter vosMessageRouter;

        public bool encryptMessages;

        public string address;

        [Range(1, 65535)]
        public int port;

        private static VOSManager instance;

        /*private void Start()
        {
            instance = this;

            if (vos == null)
            {
                Debug.LogError("[VOSManager] VOS not set.");
                return;
            }

            if (vosMessageRouter == null)
            {
                Debug.LogError("[VOSManager] VOS Message Router not set.");
                return;
            }

            vos.encryptMessages = encryptMessages;
            vos.address = address;
            vos.port = port;
            vos.onAPIMessage = new Action<VOSMessage>(vosMessageRouter.RouteMessage);
            vos.Initialize();
        }*/

        public void SendMessage(VOSMessage message)
        {
            vos.SendMessage(message);
        }
    }
}