using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PathCreator))]
    public class PathEditor : UnityEditor.Editor 
    {
        private PathCreator creator;
        private Path path = null;
        
        [Range(2, 100)] private int controlPoints = 2;
        private int distance = 5, count = 4;
        private const string HelpBoxText = "\nYou can create your own paths using this editor.\n";
        
        private void OnEnable()
        {
            creator = (PathCreator)target;
        }
        
        public override void OnInspectorGUI()
        {
            DrawMyInspector();
        }
        
        private void OnSceneGUI()
        {
            Input();
            DrawMyScene();
        }

        private void Input()
        {
            var guiEvent = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
            var distanceToDrawPlane = -ray.origin.y / ray.direction.y;
            var mousePos = ray.GetPoint(distanceToDrawPlane);
            mousePos.x = Mathf.Round(mousePos.x);
            mousePos.z = Mathf.Round(mousePos.z);
            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
            {
                Undo.RecordObject(creator, "Add segment");
                path.AddSegment(mousePos);
            }
        }

        private void DrawMyScene()
        {
            if (path == null) 
                return;
            
            for (var i = 0; i < path.NumSegments; i++)
            {
                var points = path.GetPointsInSegment(i);
                Handles.color = Color.black;
                // Handles.DrawLine(points[1], points[0]);
                // Handles.DrawLine(points[2], points[3]);
                Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
            }
            
            Handles.color = Color.red;
            for (var i = 0; i < path.NumPoints; i += 3)
            {
                Vector3 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
                if (path[i] != newPos)
                {
                    Undo.RecordObject(creator, "Move point");
                    path.MovePoint(i, newPos);
                }
            }
        }
        
        private void DrawMyInspector()
        {
            EditorGUILayout.Space(); GUILayout.Label("Create Your Path", EditorStyles.boldLabel); EditorGUILayout.Space();
            
            controlPoints = (int)EditorGUILayout.Slider("Control Points", controlPoints, 2, 100);
            EditorGUILayout.HelpBox("Green: Left lane, Blue: Right lane", MessageType.None);
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Distance", GUILayout.Width(60)); 
            distance = EditorGUILayout.IntField(distance, GUILayout.Width(50));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Count", GUILayout.Width(60)); 
            count = EditorGUILayout.IntField(count, GUILayout.Width(50)); GUILayout.FlexibleSpace();GUILayout.FlexibleSpace();GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            DrawButtons();
            
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox(HelpBoxText, MessageType.None);
            // EditorUtility.SetDirty(path);
        }

        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Distance Mode"))
                DistanceMode();
            if (GUILayout.Button("Count Mode"))
                CountMode();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Cancel"))
                Cancel();
        }
        
        private void DistanceMode()
        {
            CountMode();
        }
        
        private void CountMode()
        {
            creator.CreatePath(distance, count);
            path = creator.path;
        }

        private void Cancel()
        {
            path = null;
        }
        
    }
}
