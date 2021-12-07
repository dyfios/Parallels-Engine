using System;
using System.Diagnostics;
using UnityEngine;
using FiveSQD.Parallels.HTTP;
using FiveSQD.Libraries.VOSMessenger;

namespace FiveSQD.Parallels.Daemon
{
    public class DaemonManager : MonoBehaviour
    {
        private static readonly string daemonExecutableName = "parallels-daemon.exe";
        private static readonly string daemonExecutableArguments = "";
        private static readonly string daemonProcessName = "parallels-daemon";

        private static DaemonManager instance;

        private bool needToConnect = false;
        private string pathToKey = "";
        private string vosHost = "";
        private uint vosPort = 0;
        private uint triesRemaining = 0;
        private float connectInterval;

        public void Initialize(string keyPath, string host, uint port, uint connectionTries = 8, float connectionInterval = 1)
        {
            instance = this;
            if (IsDaemonRunning())
            {
                UnityEngine.Debug.Log("[DaemonManager->Initialize] Daemon is already running,"
                    + " attempting to connect.");
            }
            else
            {
                UnityEngine.Debug.Log("[DaemonManager->Initialize] Daemon is not running,"
                    + " attempting to start.");
                Process daemonProcess = StartDaemon();
            }
            pathToKey = keyPath;
            vosHost = host;
            vosPort = port;
            triesRemaining = connectionTries;
            connectInterval = connectionInterval;
            needToConnect = true;
        }

        public static bool IsDaemonRunning()
        {
            try
            {
                Process[] daemonProc = Process.GetProcessesByName(daemonProcessName);
                if (daemonProc.Length > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("[DaemonManager->IsDaemonRunning] " + e.ToString());
            }
            return false;
        }

        public static Process StartDaemon()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(daemonExecutableName);
                startInfo.Arguments = daemonExecutableArguments;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                return Process.Start(startInfo);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("[DaemonManager->StartDaemon] " + e.ToString());
            }
            return null;
        }

        public static void StopDaemon()
        {
            try
            {
                Process[] daemonProc = Process.GetProcessesByName(daemonProcessName);
                foreach (Process proc in daemonProc)
                {

                    proc.Kill();
                    proc.WaitForExit();
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("[DaemonManager->StopDaemon] " + e.ToString());
            }
        }

        public static void ConnectToDaemon(string publicKeyPath, string host, uint httpPort)
        {
            try
            {
                if (IsDaemonRunning())
                {
                    if (instance == null)
                    {
                        throw new Exception("No DaemonManager instance.");
                    }
                    instance.StartCoroutine(VOSHTTP.RequestDaemonVOSPort(publicKeyPath, OnVOSPortResponse, host, httpPort));
                }
                else
                {
                    throw new Exception("Daemon is not running.");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("[DaemonManager->ConnectToDaemon] " + e.ToString());
            }
        }

        public static bool IsConnectedToDaemon()
        {
            // TODO.
            return false;
        }

        public static void OnVOSPortResponse(uint port)
        {
            // TODO.
        }

        private float timeElapsed = 0;
        private void Update()
        {
            if (triesRemaining > 0 && !IsConnectedToDaemon())
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= connectInterval)
                {
                    ConnectToDaemon(pathToKey, vosHost, vosPort);
                    triesRemaining--;
                    timeElapsed = 0;
                }
            }
            else
            {
                triesRemaining = 0;
            }
        }
    }
}