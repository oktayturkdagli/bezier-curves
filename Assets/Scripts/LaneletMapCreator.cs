using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class LaneletMapCreator : MonoBehaviour 
    {
        public List<LaneletMap> LaneletMaps { get; set; } = new List<LaneletMap>();

        public void AddLaneletMapDefault(Vector3 position = default)
        {
            var text = "CreateLaneletMapDefault";
            UtilityManager.LogMessage<string>(ref text);
            
            // Width and Node Count
            float width = 5; 
            int nodeCount = 10;
            
            // Add default way
            LaneletMap laneletMap = new LaneletMap();
            LaneletMaps.Add(laneletMap);
            Lanelet lanelet = new Lanelet();
            laneletMap.AddLanelet(lanelet);
            Way way1 = new Way();
            lanelet.AddWay(way1);
            Way way2 = new Way();
            lanelet.AddWay(way2);
            
            // Detect Origin
            Vector3 defaultOrigin = Vector3.zero;
            
            // Add Nodes
            for (var i = 0; i < lanelet.Ways.Count; i++)
            {
                var direction = i % 2 == 0 ? -1 : 1 ;
                Vector3 origin = (defaultOrigin + Vector3.right * width / 2) * direction;
                origin.y = 0;
                
                Way way = lanelet.Ways[i];
                
                NodeAnchor node1 = new NodeAnchor(origin + Vector3.back);
                NodeAnchor node2 = new NodeAnchor(origin + Vector3.forward);
                node1.AddControlNode(new NodeControl(origin + Vector3.back * 0.5f));
                node2.AddControlNode(new NodeControl(origin + Vector3.forward * 0.5f));
                way.AddNode(node1);
                way.AddNode(node2);
                
                for (var j = 2; j < nodeCount; j++) // Add other segments
                {
                    Vector3 newPosition = origin +  j * Vector3.forward;
                    AddSegments(newPosition, way);
                }
            }
        }
        
        public void AddLaneletMap()
        {
            var text = "CreateLaneletMap";
            UtilityManager.LogMessage<string>(ref text);
            
        }

        private void AddSegments(Vector3 newPosition, Way way)
        {
            List<NodeAnchor> wayNodes = way.Nodes;
            newPosition.y = 0;
            NodeAnchor lastNode = wayNodes.Last();
            NodeControl newControlNode1 = new NodeControl(lastNode.Position + (lastNode.Position - lastNode.ControlNodes.Last().Position)); // Second control point is adding for previous last Anchor
            lastNode.AddControlNode(newControlNode1);
            NodeAnchor newAnchorNode = new NodeAnchor(newPosition); // New Anchor (Which is last Anchor now) is adding
            NodeControl newControlNode2 = new NodeControl((lastNode.ControlNodes.Last().Position + newAnchorNode.Position) * 0.5f); // New control point is adding for last Anchor
            newAnchorNode.AddControlNode(newControlNode2);
            way.AddNode(newAnchorNode);
        }
        
        public void RemoveLaneletMap()
        {
            var text = "RemoveLaneletMap";
            UtilityManager.LogMessage<string>(ref text);

            LaneletMaps.Clear();
        }
    }
}