using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class Way : Element
    {
        public List<NodeAnchor> Nodes { get; set; }
        
        public Way()
        {
            Nodes = new List<NodeAnchor>();
        }

        public void AddNode(NodeAnchor node)
        {
            NodeAnchor tempNode = Nodes.FirstOrDefault(element => element.Id == node.Id);
            if (tempNode != null) Nodes.Remove(tempNode);

            node.AddOwner(Id);
            Nodes.Add(node);
        }

        public void RemoveNode(NodeAnchor node)
        {
            NodeAnchor tempNode = Nodes.FirstOrDefault(element => element.Id == node.Id);
            if (tempNode != null) Nodes.Remove(tempNode);
        }
        
        //TODO: Move another class
        public void MoveAnchorNode(int nodeIndex, Vector3 position)
        {
            position.y = 0;
            List<NodeAnchor> nodeList = Nodes;
            Vector3 deltaMove = position - nodeList[nodeIndex].Position;
            NodeAnchor currentNode = nodeList[nodeIndex];
            currentNode.Position = position;
            for (int i = 0; i < currentNode.ControlNodes.Count; i++)
            {
                currentNode.ControlNodes[i].Position += deltaMove;
            }

        }
        
        public void MoveControlNode(int nodeIndex, Vector3 position)
        {
            
        }
    }
}