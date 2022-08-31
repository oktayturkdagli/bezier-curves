using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class Way
    {
        private int id;
        private List<int> owners = new List<int>(); // Lanelet id's, which have this way
        private List<NodeAnchor> nodes = new List<NodeAnchor>();

        public int ID
        {
            get => id;
            set => id = value;
        }

        public List<int> Owners
        {
            get => owners;
            set => owners = value;
        }

        public List<NodeAnchor> Nodes
        {
            get => nodes;
            set => nodes = value;
        }

        public Way()
        {
            id = NumberManager.WayId++;
        }

        public void AddOwner(int newOwner)
        {
            bool isAlreadyOnList = owners.Contains(newOwner);
            if (isAlreadyOnList)
                return;

            owners.Add(newOwner);
        }

        public void RemoveOwner(int owner)
        {
            bool isOnList = owners.Contains(owner);
            if (!isOnList)
                return;

            owners.Remove(owner);
        }

        public void AddNodeDefault(bool toTheLeft = true)
        {
            // float offset = toTheLeft ? -2.5f : 2.5f;
            // Vector3 position = new Vector3(offset, 0, 0);
            // for (int i = 0; i < 5; i++)
            // {
            //     position.z = i;
            //     Node node = new Node(position);
            //     AddNode(node);
            // }
        }

        public void AddNode(NodeAnchor node)
        {
            NodeAnchor tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) nodes.Remove(tempNode);

            node.AddOwner(id);
            nodes.Add(node);
        }

        public void RemoveNode(NodeAnchor node)
        {
            NodeAnchor tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) nodes.Remove(tempNode);
        }
        
        //TODO: Move another class
        public void MoveAnchorNode(int nodeIndex, Vector3 position)
        {
            position.y = 0;
            List<NodeAnchor> nodeList = nodes;
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