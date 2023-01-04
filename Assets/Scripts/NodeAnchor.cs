using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class NodeAnchor : Node
    {
        public List<NodeControl> ControlNodes { get; set; }

        public override void Init()
        {
            base.Init();
            ControlNodes = new List<NodeControl>();
            Type = ElementTypes.AnchorNode;
        }
        
        public NodeAnchor(Vector3 position) : base(position) { }
        
        public NodeControl AddControlNode(Vector3 position = default)
        {
            if (ControlNodes.Count > 1)
                return null;
            
            var text = "AddControlNode";
            UtilityManager.LogMessage<string>(ref text);
            
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.transform.parent = transform;
            newObj.transform.localPosition = position;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.transform.localScale = Vector3.one * 0.5f;
            NodeControl node = newObj.AddComponent<NodeControl>();
            newObj.name = UtilityManager.NameChanger(node.Id, ElementTypes.ControlNode);
            
            node.Init();
            newObj.GetComponent<Renderer>().sharedMaterial = UtilityManager.MaterialRequest(node.Type);
            node.AddOwner(this);
            ControlNodes.Add(node);
            return node;
        }
        
        public void RemoveControlNode(NodeControl node)
        {
            NodeControl tempNode = ControlNodes.FirstOrDefault(element => element.Id == node.Id);
            if (tempNode != null) ControlNodes.Remove(tempNode);
        }
    }
}