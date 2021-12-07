using UnityEngine;
using System.Collections.Generic;
using FiveSQD.Libraries.VOSMessenger;

namespace FiveSQD.Parallels.Runtime.Engine.VOSRouting
{
    public class VOSMessageRouter : MonoBehaviour
    {
        [System.Serializable]
        public class RoutingTableEntry
        {
            public string key;
            public VOSRoutable route;
        }

        public int maximumMessagesPerFrame = 4;
        
        [SerializeField]
        public List<RoutingTableEntry> routingTable;

        private Queue<VOSMessage> pendingMessages;

        public void RouteMessage(VOSMessage message)
        {
            pendingMessages.Enqueue(message);
        }

        private VOSRoutable FindRoute(string subject)
        {
            string[] parts = subject.Split('/');

            if (parts.Length > 1)
            {
                string currentPrefix = parts[0];
                VOSRoutable match = null;// routingTable[currentPrefix];
                if (match != null)
                {
                    return match;
                }

                for (int i = 1; i < parts.Length; i++)
                {
                    currentPrefix = currentPrefix + "/" + parts[i];
                    //match = routingTable[currentPrefix];
                    if (match != null)
                    {
                        return match;
                    }
                }
            }

            return null;
        }

        private void Start()
        {
            pendingMessages = new Queue<VOSMessage>();
        }

        private void Update()
        {
            int numMessages = 0;
            while (pendingMessages.Count > 0 && numMessages++ < maximumMessagesPerFrame)
            {
                VOSMessage currentMessage = pendingMessages.Dequeue();
                VOSRoutable routable = FindRoute(currentMessage.topic);
                if (routable == null)
                {
                    Debug.LogWarning("[VOSMessageRouter] Unidentified topic " + currentMessage.topic + ".");
                }
                else
                {
                    routable.HandleMessage(currentMessage);
                }
            }
        }
    }
}