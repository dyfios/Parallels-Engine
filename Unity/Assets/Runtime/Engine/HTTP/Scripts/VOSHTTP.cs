using System;
using System.Collections;
using System.IO;

namespace FiveSQD.Parallels.HTTP
{
    public class VOSHTTP
    {
        public static readonly string GETDAEMONVOSPATH = "get-vos-connection";

        public static IEnumerator RequestDaemonVOSPort(string keyPath,
            Action<uint> onResponse, string daemonHTTPHost = "localhost", uint daemonHTTPPort = 5525)
        {
            string publicKey = File.ReadAllText(keyPath);
            if (string.IsNullOrEmpty(keyPath))
            {
                UnityEngine.Debug.LogError("[VOSHTTP->GetDaemonVOSPort] Invalid public key path: " + keyPath);
                yield break;
            }

            UnityEngine.Networking.UnityWebRequest wr = UnityEngine.Networking.UnityWebRequest.Get(
                "https://" + daemonHTTPHost + ":" + daemonHTTPPort + "/" + GETDAEMONVOSPATH);
            wr.certificateHandler = new CertificateChecker(publicKey);
            yield return wr.SendWebRequest();

            switch (wr.result)
            {
                case UnityEngine.Networking.UnityWebRequest.Result.ConnectionError:
                case UnityEngine.Networking.UnityWebRequest.Result.DataProcessingError:
                case UnityEngine.Networking.UnityWebRequest.Result.ProtocolError:
                    UnityEngine.Debug.LogError("[VOSHTTP->GetDaemonVOSPort] Error: " + wr.error);
                    break;

                case UnityEngine.Networking.UnityWebRequest.Result.Success:
                    if (onResponse != null)
                    {
                        onResponse.Invoke(uint.Parse(wr.downloadHandler.text));
                    }
                    break;
            }
        }
    }
}