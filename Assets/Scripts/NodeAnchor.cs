using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    public class NodeAnchor : Node
    {
        public List<NodeControl> ControlNodes { get; set; }
        
        public NodeAnchor(Vector3 position) : base(position)
        {
            ControlNodes = new List<NodeControl>();
        }

        public void AddControlNode(NodeControl node)
        {
            NodeControl tempNode = ControlNodes.FirstOrDefault(element => element.Id == node.Id);
            if (tempNode != null) ControlNodes.Remove(tempNode);
            
            if (ControlNodes.Count > 1)
                return;

            node.AddOwner(Id);
            ControlNodes.Add(node);
        }

        public void RemoveControlNode(NodeControl node)
        {
            NodeControl tempNode = ControlNodes.FirstOrDefault(element => element.Id == node.Id);
            if (tempNode != null) ControlNodes.Remove(tempNode);
        }
        
    }
}