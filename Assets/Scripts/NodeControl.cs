using UnityEngine;

namespace LaneletProject
{
    public class NodeControl : Node
    {
        private int id;
        
        public int ID
        {
            get => id;
            set => id = value;
        }
        
        public NodeControl(Vector3 position) : base(position)
        {
            id = NumberManager.NodeControlId++;
        }
    }
}