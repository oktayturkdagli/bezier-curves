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
        private const string HelpBoxText = "\nYou can create your own paths using this editor.\n" + 
                                           "\nYou can add new segments by pressing Shift + Mouse Left Click.\n";
        
        private void OnEnable()
        {
            creator = (PathCreator)target;
            if (creator.path == null)
            {
                creator.CreatePath(distance, count);
            }
            path = creator.path;
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
                count++;
                path.AddSegment(mousePos, distance);
            }
            
            HandleUtility.AddDefaultControl(0);
        }

        private void DrawMyScene()
        {
            if (path == null) 
                return;

            DrawMyPoints(PointType.LeftPoint);
            DrawMyPoints(PointType.RightPoint);
        }
        
        private void DrawMyPoints(PointType pointType)
        {
            for (var i = 0; i < path.NumSegments(pointType); i++)
            {
                var points = path.GetPointsInSegment(i, pointType);
                Handles.color = Color.black;
                // For a more detailed bezier implementation, check out the Path.cs/CalculateEvenlySpacedPoints function.
                Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
            }
            
            Handles.color = Color.red;
            for (var i = 0; i < path.NumPoints(pointType); i += 3)
            {
                var newPos = Handles.FreeMoveHandle(path.Point(i, pointType), Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
                if (path.Point(i, pointType)!= newPos)
                {
                    Undo.RecordObject(creator, "Move point");
                    path.MovePoint(i, newPos, pointType);
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
            DrawMyButtons();
            
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox(HelpBoxText, MessageType.None);
            EditorUtility.SetDirty(creator);
        }

        private void DrawMyButtons()
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Distance Mode"))
            {
                Undo.RecordObject(creator, "Distance Mode");
                DistanceMode();
            }
            if (GUILayout.Button("Count Mode"))
            {
                Undo.RecordObject(creator, "Count Mode");
                CountMode();
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Cancel"))
            {
                Undo.RecordObject(creator, "Cancel");
                Cancel();
            }

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }
        
        private void DistanceMode()
        {
            if (path == null)
                return;
        }
        
        private void CountMode()
        {
            if (path == null)
            {
                creator.CreatePath(distance, count);
                path = creator.path;
            }
        }

        private void Cancel()
        {
            creator.path = null;
            path = null;
        }
        
    }
}
