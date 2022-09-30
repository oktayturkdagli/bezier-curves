using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(IdManager))]
    public class LaneletMapCreator : MonoBehaviour
    {
        public List<LaneletMap> LaneletMaps { get; set; }
        
        private void Start()
        {
            LaneletMaps = new List<LaneletMap>();
            gameObject.name = GetType().Name;
        }

        public void AddLaneletMapDefault(Vector3 position = default)
        {
            // Width and Node Count
            float width = 5; 
            int nodeCount = 10;

            // Add default way
            LaneletMap laneletMap = AddLaneletMap();
            Lanelet lanelet = laneletMap.AddLanelet();
            Vector3 way1Position = Vector3.left * width / 2;
            Vector3 way2Position = Vector3.right * width / 2;
            Way way1 = lanelet.AddWay(way1Position);
            Way way2 = lanelet.AddWay(way2Position);
            
            // Add Nodes
            for (var i = 0; i < lanelet.Ways.Count; i++)
            {
                Vector3 origin = Vector3.zero;
                Way way = lanelet.Ways[i];
                
                NodeAnchor node1 = way.AddNode(origin + Vector3.back);
                NodeAnchor node2 = way.AddNode(origin + Vector3.forward);
            
                NodeControl controlNode1 = node1.AddControlNode(origin + Vector3.back * 0.5f);
                NodeControl controlNode2 = node2.AddControlNode(origin + Vector3.forward * 0.5f);
            
                for (var j = 2; j < nodeCount; j++) // Add other segments
                {
                    Vector3 newPosition = origin +  j * Vector3.forward;
                    AddSegments(newPosition, way);
                }
            }
        }
        
        public LaneletMap AddLaneletMap(Vector3 position = default)
        {
            var text = "CreateLaneletMap";
            UtilityManager.LogMessage<string>(ref text);
            
            GameObject newObj = new GameObject("New Object");
            newObj.transform.parent = transform;
            newObj.transform.localPosition = position;
            newObj.transform.localRotation = Quaternion.identity;
            LaneletMap laneletMap = newObj.AddComponent<LaneletMap>();
            newObj.name = UtilityManager.NameChanger(laneletMap.Id, ElementTypes.LaneletMap);
            
            laneletMap.Init();
            LaneletMaps.Add(laneletMap);
            return laneletMap;
        }

        private void AddSegments(Vector3 newPosition, Way way)
        {
            List<NodeAnchor> wayNodes = way.Nodes;
            newPosition.y = 0;
            NodeAnchor lastNode = wayNodes.Last();
            NodeControl newControlNode1 = lastNode.AddControlNode(lastNode.Position + (lastNode.Position - lastNode.ControlNodes.Last().Position)); // Second control point is adding for previous last Anchor
            NodeAnchor newAnchorNode = way.AddNode(newPosition); // New Anchor (Which is last Anchor now) is adding
            NodeControl newControlNode2 = newAnchorNode.AddControlNode((lastNode.ControlNodes.Last().Position + newAnchorNode.Position) * 0.5f); // New control point is adding for last Anchor
        }
        
        public void RemoveLaneletMap()
        {
            var text = "RemoveLaneletMap";
            UtilityManager.LogMessage<string>(ref text);

            LaneletMaps.Clear();
        }
    }
}