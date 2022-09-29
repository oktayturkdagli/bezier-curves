using UnityEngine;

namespace LaneletProject
{
    [System.Serializable]
    public class Node : Element
    {
        public Vector3 Position { get; set; }
        
        public Node(Vector3 position)
        { 
            Position = position;
        }
    }
}

