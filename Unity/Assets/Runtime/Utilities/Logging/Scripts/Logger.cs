using UnityEngine;

namespace FiveSQD.Utilities
{
    public class LogSystem
    {
        public enum Type { Default, Debug, Warning, Error };

        public static void Log(string message, Type type = Type.Default)
        {
            // Forward to Unity's Logging System.
            switch (type)
            {
                case Type.Debug:
                    // TODO: Only in development build.
                    Debug.Log(message);
                    break;

                case Type.Warning:
                    Debug.LogWarning(message);
                    break;

                case Type.Error:
                    Debug.LogError(message);
                    break;

                case Type.Default:
                default:
                    Debug.Log(message);
                    break;
            }

            // Log to consoles.
            //Parallels.Infrastructure.ConsoleCommandParser.LogMessage(message, type);
        }

        public static void LogDebug(string message)
        {
            Log(message, Type.Debug);
        }

        public static void LogWarning(string message)
        {
            Log(message, Type.Warning);
        }

        public static void LogError(string message)
        {
            Log(message, Type.Error);
        }
    }
}