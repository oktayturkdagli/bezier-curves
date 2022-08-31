using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class LaneletMapCreator : MonoBehaviour 
    {
        private int id;
        private List<LaneletMap> laneletMaps = new List<LaneletMap>();

        public int ID { get => id; set => id = value; }
        public List<LaneletMap> LaneletMaps { get => laneletMaps; set => laneletMaps = value; }

        public void AddLaneletMapDefault(Vector3 position = default)
        {
            var text = "CreateLaneletMapDefault";
            Utilities.LogMessage<string>(ref text);
            
            LaneletMap laneletMap = new LaneletMap();
            laneletMaps.Add(laneletMap);
            laneletMap.AddLaneletDefault();
        }
        
        public void AddLaneletMap(float width = 5, int nodeCount = 5)
        {
            var text = "CreateLaneletMap";
            Utilities.LogMessage<string>(ref text);

            LaneletMap laneletMap = new LaneletMap();
            laneletMaps.Add(laneletMap);
            Lanelet lanelet = new Lanelet();
            laneletMap.AddLanelet(lanelet);
            Way way1 = new Way();
            lanelet.AddWay(way1);
            Way way2 = new Way();
            lanelet.AddWay(way2);

            Vector3 defaultCenter = Vector3.zero;

            for (var i = 0; i < lanelet.Ways.Count; i++)
            {
                var direction = i % 2 == 0 ? -1 : 1 ;
                Vector3 center = (defaultCenter + Vector3.right * width / 2) * direction;
                center.y = 0;

                Way way = lanelet.Ways[i];
                
                NodeAnchor node1 = new NodeAnchor(center + Vector3.back);
                NodeAnchor node2 = new NodeAnchor(center + Vector3.forward);
                node1.AddControlNode(new NodeControl(center + (Vector3.back + Vector3.left) * 0.5f));
                node2.AddControlNode(new NodeControl(center + (Vector3.forward + Vector3.right) * 0.5f));
                way.AddNode(node1);
                way.AddNode(node2);
                
                for (var j = 2; j < 10; j++) // Add other segments
                {
                    Vector3 newPosition = center +  j * Vector3.forward;
                    AddSegments(newPosition, way);
                }
            }
        }

        public void AddSegments(Vector3 newPosition, Way way)
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
            Utilities.LogMessage<string>(ref text);

            laneletMaps.Clear();
        }
    }
}