using UnityEngine;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class Utilities : MonoBehaviour
    {
        public static void LogMessage<T>(ref T message)
        {
            #if UNITY_EDITOR
            print(message);
            #endif
        }

    }
}