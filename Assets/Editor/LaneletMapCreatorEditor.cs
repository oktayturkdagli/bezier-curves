using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LaneletProject;

namespace Editor
{
    [CustomEditor(typeof(LaneletMapCreator))]
    public class LaneletMapCreatorEditor : UnityEditor.Editor
    {
        private LaneletMapCreator laneletMapCreator => (LaneletMapCreator) target;

        public override void OnInspectorGUI() //TODO: Always opened hierarchy
        {
            DrawMyInspector();
        }
        
        private void OnSceneGUI()
        {
            // DrawMyScene();
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
            EditorGUILayout.HelpBox("All operations related to Laneletmap are in this panel.", MessageType.None);
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
                        List<NodeAnchor> wayNodes = way.Nodes;
                        for (var l = 0; l < wayNodes.Count; l++)
                        {
                            NodeAnchor currentNode = wayNodes[l];
                            NodeAnchor nextNode;
                            if (l == 0)
                            {
                                nextNode = wayNodes[l + 1];
                                NodeControl controlNode1 = currentNode.ControlNodes[0];
                                NodeControl controlNode2 = nextNode.ControlNodes[0];
                                Handles.DrawBezier(currentNode.Position, nextNode.Position, controlNode1.Position, controlNode2.Position, Color.green, null, 2);
                            }
                            else if (l  == wayNodes.Count - 1)
                            {
                                //Do nothing
                            }
                            else
                            {
                                nextNode = wayNodes[l + 1];
                                NodeControl controlNode1 = currentNode.ControlNodes[1];
                                NodeControl controlNode2 = nextNode.ControlNodes[0];
                                Handles.DrawBezier(currentNode.Position, nextNode.Position, controlNode1.Position, controlNode2.Position, Color.green, null, 2);
                            }
                        }
                        
                        for (var m = 0; m < wayNodes.Count; m++)
                        {
                            Handles.color = Color.red;
                            NodeAnchor anchorNode = wayNodes[m];
                            var newAnchorPos = Handles.FreeMoveHandle(anchorNode.Position, Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
                            if (anchorNode.Position != newAnchorPos)
                            {
                                Undo.RecordObject(this, "Move Anchor Node");
                                way.MoveAnchorNode(m, newAnchorPos);
                            }
                            
                            Handles.color = Color.blue;
                            for (int n = 0; n < anchorNode.ControlNodes.Count; n++)
                            {
                                NodeControl controlNode = anchorNode.ControlNodes[n];
                                var newControlPos = Handles.FreeMoveHandle(controlNode.Position, Quaternion.identity, .05f, Vector3.zero, Handles.CylinderHandleCap);
                                if (controlNode.Position != newControlPos)
                                {
                                    Undo.RecordObject(this, "Move Control Node");
                                    way.MoveControlNode(n, newControlPos);
                                }
                            }
                        }
                    }

                }
            }
            
        }
        
    }
}