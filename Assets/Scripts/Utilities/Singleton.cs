using UnityEngine;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance)
                    return instance;

                instance = FindObjectOfType<T>();
                if (!instance)
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();

                return instance;
            }
        }

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                DestroyImmediate(this);
            }
        }
    }
}