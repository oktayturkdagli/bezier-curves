using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class Way : Element
    {
        public List<NodeAnchor> Nodes { get; set; }
        
        public override void Init()
        {
            base.Init();
            Nodes = new List<NodeAnchor>();
            Type = ElementTypes.Way;
        }
        
        public NodeAnchor AddNode(Vector3 position = default)
        {
            var text = "AddNode";
            UtilityManager.LogMessage<string>(ref text);
            
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.transform.parent = transform;
            newObj.transform.localPosition = position;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.transform.localScale = Vector3.one * 0.1f;
            NodeAnchor node = newObj.AddComponent<NodeAnchor>();
            newObj.name = UtilityManager.NameChanger(node.Id, ElementTypes.AnchorNode);
            
            node.Init();
            newObj.GetComponent<Renderer>().sharedMaterial = UtilityManager.MaterialRequest(node.Type);
            node.AddOwner(this);
            Nodes.Add(node);
            return node;
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