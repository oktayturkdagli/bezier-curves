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
        private List<Node> nodes = new List<Node>();

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

        public List<Node> Nodes
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

        public void AddNode(Node node)
        {
            Node tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) nodes.Remove(tempNode);

            node.AddOwner(id);
            nodes.Add(node);
        }

        public void RemoveNode(Node node)
        {
            Node tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
            if (tempNode != null) nodes.Remove(tempNode);

            nodes.Remove(node);
        }

        public Node GetNode(int index)
        {
            if (index >= nodes.Count)
                return null;

            return nodes[index];
        }

        public Node[] GetRealNodes(int index)
        {
            return new Node[] { nodes[index * 3], nodes[index * 3 + 1], nodes[index * 3 + 2], nodes[index * 3 + 3] };
        }

        public int NumSegments()
        {
            // Constructor segment has 4 point but we count as 1 and other anchors has 3 element (Itself and 2 control point)
            return nodes.Count / 3;
        }

        public void MovePoint(int i, Vector3 pos)
        {
            pos.y = 0;
            var nodeList = nodes;

            Vector3 deltaMove = pos - nodeList[i].Position;
            nodeList[i].Position = pos;

            if (i % 3 == 0)
            {
                if (i + 1 < nodeList.Count)
                {
                    nodeList[i + 1].Position += deltaMove;
                }

                if (i - 1 >= 0)
                {
                    nodeList[i - 1].Position += deltaMove;
                }
            }
            else
            {
                bool nextPointIsAnchor = (i + 1) % 3 == 0;
                int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
                int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

                if (correspondingControlIndex >= 0 && correspondingControlIndex < nodeList.Count)
                {
                    float dst = (nodeList[anchorIndex].Position - nodeList[correspondingControlIndex].Position)
                        .magnitude;
                    Vector3 dir = (nodeList[anchorIndex].Position - pos).normalized;
                    nodeList[correspondingControlIndex].Position = nodeList[anchorIndex].Position + dir * dst;
                }
            }
        }

    }
}