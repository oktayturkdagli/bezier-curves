using UnityEngine;
using System.Collections.Generic;
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
                var nodes = new List<Node>
                {
                    new NodeAnchor(center + Vector3.back),
                    new NodeControl(center + (Vector3.back + Vector3.left) * 0.5f),
                    new NodeControl(center + (Vector3.forward + Vector3.right) * 0.5f),
                    new NodeAnchor(center + Vector3.forward)
                };

                for (var j = 0; j < nodes.Count; j++)
                {
                    way.AddNode(nodes[j]);
                }
                
                for (var j = 0; j < nodeCount - 2; j++) // Add other segments
                {
                    Vector3 newPosition = center + Vector3.forward * (j + 2);
                    AddSegments(newPosition, way);
                }
                
            }
        }

        public void AddSegments(Vector3 newPosition, Way way)
        {
            List<Node> wayNodes = way.Nodes;
            newPosition.y = 0;
            way.AddNode(new NodeControl(wayNodes[wayNodes.Count - 1].Position * 2 - wayNodes[wayNodes.Count - 2].Position)); // Second control point is adding for previous last Anchor
            way.AddNode(new NodeControl((wayNodes[wayNodes.Count - 1].Position + newPosition) * 0.5f)); // New control point is adding for last Anchor
            way.AddNode(new NodeAnchor(newPosition)); // New Anchor (Which is last Anchor now) is adding
        }
        
        public void RemoveLaneletMap()
        {
            var text = "RemoveLaneletMap";
            Utilities.LogMessage<string>(ref text);

            laneletMaps.Clear();
        }
    }
}