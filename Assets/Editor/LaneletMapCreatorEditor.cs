using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LaneletMapCreator))]
    public class LaneletMapCreatorEditor : UnityEditor.Editor
    {
        private LaneletMapCreator laneletMapCreator;
        
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
                laneletMapCreator.AddLaneletMapDefault();
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
            
        }

    }
}