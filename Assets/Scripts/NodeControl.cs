using UnityEngine;

namespace LaneletProject
{
    [System.Serializable]
    public class NodeControl : Node
    {
        public NodeControl(Vector3 position) : base(position) { }
        public override void Init()
        {
            base.Init();
            Type = ElementTypes.ControlNode;
        }
    }
}