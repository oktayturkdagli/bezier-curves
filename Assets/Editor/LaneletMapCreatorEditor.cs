using UnityEngine;
using UnityEditor;
using LaneletProject;

namespace Editor
{
    [CustomEditor(typeof(LaneletMapCreator))]
    public class LaneletMapCreatorEditor : UnityEditor.Editor
    {
        private LaneletMapCreator laneletMapCreator = null;
        
        private void OnEnable()
        {
            laneletMapCreator = (LaneletMapCreator) target;
        }

        public override void OnInspectorGUI()
        {
            DrawMyInspector();
        }
        
        private void OnSceneGUI()
        {
            DrawMyScene();
        }

        private void DrawMyInspector()
        {
            EditorGUILayout.Space(); GUILayout.Label("Lanelet Creator", EditorStyles.boldLabel); EditorGUILayout.Space();
            GUILayout.Space(10);
            
            EditorGUI.BeginChangeCheck();
            if (GUILayout.Button("Add Default Lanelet"))
            {
                Undo.RecordObject(this, "Add Default Lanelet");
                laneletMapCreator.AddLaneletMapDefault();
            }
            else if (GUILayout.Button("Add Lanelet"))
            {
                Undo.RecordObject(this, "Add Lanelet");
                laneletMapCreator.AddLaneletMap();
            }
            else if (GUILayout.Button("Remove"))
            {
                Undo.RecordObject(this, "Remove Lanelet");
                laneletMapCreator.RemoveLaneletMap();
            }

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
            
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox("Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia, molestiae quas", MessageType.None);
        }
        
        private void DrawMyScene()
        {
            if (laneletMapCreator.LaneletMaps.Count < 1)
             return;
            
            for (var i = 0; i < laneletMapCreator.LaneletMaps.Count; i++)
            {
                LaneletMap laneletMap = laneletMapCreator.LaneletMaps[i];
                for (var j = 0; j < laneletMap.Lanelets.Count; j++)
                {
                    Lanelet lanelet = laneletMap.Lanelets[j];
                    for (var k = 0; k < lanelet.Ways.Count; k++)
                    {
                        Way way = lanelet.Ways[k];
                        Handles.color = Color.black;
                        for (var l = 0; l < way.NumSegments(); l++)
                        {
                            Node[] nodes = way.GetRealNodes(l);
                            Handles.DrawBezier(nodes[0].Position, nodes[3].Position, nodes[1].Position, nodes[2].Position, Color.green, null, 2);
                        }
                        
                        Handles.color = Color.red;
                        for (var m = 0; m < way.Nodes.Count; m += 3)
                        {
                            var newPos = Handles.FreeMoveHandle(way.GetNode(m).Position, Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
                            if (way.GetNode(m).Position != newPos)
                            {
                                Undo.RecordObject(this, "Move point");
                                way.MovePoint(m, newPos);
                            }
                        }
                    }

                }
            }
            
        }
        
        
    }
}