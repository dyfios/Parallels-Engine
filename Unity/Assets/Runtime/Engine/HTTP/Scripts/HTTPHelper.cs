using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using FiveSQD.Utilities;

namespace FiveSQD.Parallels.HTTP
{
    public class HTTPHelper : MonoBehaviour
    {
        public static IEnumerator GetRequest(string uri, Action<long, byte[]> onResult)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    LogSystem.LogError("[HTTPHelper->GetRequest] Error: " + uwr.error);
                    if (onResult != null)
                    {
                        onResult.Invoke(uwr.responseCode, null);
                    }
                    break;

                case UnityWebRequest.Result.Success:
                    if (onResult != null)
                    {
                        onResult.Invoke(uwr.responseCode, uwr.downloadHandler.data);
                    }
                    break;
            }
        }
    }
}