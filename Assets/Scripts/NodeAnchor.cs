using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    public class NodeAnchor : Node
    {
        private int id;
        private List<NodeControl> controlNodes = new List<NodeControl>(); // Control nodes of this node

        public int ID
        {
            get => id;
            set => id = value;
        }
        
        public List<NodeControl> ControlNodes
        {
            get => controlNodes;
            set => controlNodes = value;
        }
        
        public NodeAnchor(Vector3 position) : base(position)
        {
            id = NumberManager.NodeAnchorId++;
        }
        
        public void AddControlNode(NodeControl node)
        {
            NodeControl tempNode = controlNodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) controlNodes.Remove(tempNode);
            
            if (controlNodes.Count > 1)
                return;

            node.AddOwner(id);
            controlNodes.Add(node);
        }

        public void RemoveControlNode(NodeControl node)
        {
            NodeControl tempNode = controlNodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) controlNodes.Remove(tempNode);
        }
    }
}