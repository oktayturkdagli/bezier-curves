using UnityEditor;
using UnityEngine;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class IdManager : MonoBehaviour
    {
        public static IdManager Instance { get; set; }

        private void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }

        public int GlobalId { get; set; } = 0;
        
        public int NodeAnchorId { get; set; } = 0;
        
        public int NodeControlId { get; set; } = 0;

        public int WayId { get; set; } = 0;

        public int LaneletId { get; set; } = 0;

        public int LaneletMapId { get; set; } = 0;

        public int TakeId()
        {
            return GlobalId++;
        }
    }
}