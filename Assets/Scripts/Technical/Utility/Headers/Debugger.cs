using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public static class Debugger
    {
        //==================================
        //=========TYPE
        //==================================

        //Debug type
        public enum DebugType : ushort
        {
            Unknown = 0,
            Network = 1,
        }
        private static readonly string[] debugNames =
        {
            "Unknown",
            "Network"
        };

        //Error type
        public enum ErrorType : ushort
        {
            UnknownError = 0,
            NullError = 1,
            SingletonError = 2,
            NetworkError = 3,
        }
        private static readonly string[] errorNames =
        {
            "UnknownError",
            "NullError",
            "SingletonError",
            "NetworkError"
        };

        //==================================
        //=========LOG
        //==================================

        //Mode
        private static readonly bool debugMode = true;

        //Log
        public static void Log(DebugType type, string source, string message)
        {
            if (debugMode)
            {
                Debug.Log(debugNames[(ushort)type] + "(" + source + "): " + message);
            }
        }

        //Exception
        public static void Throw(ErrorType type, string source, string message)
        {
            if (debugMode)
            {
                Debug.LogError(errorNames[(ushort)type] + "(" + source + "): " + message);
            }            
        }
    }
}

