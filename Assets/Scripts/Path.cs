using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path 
{
    [SerializeField, HideInInspector] private List<Vector3> points;

    public Path(Vector3 center, int distance, int count)
    {
        points = new List<Vector3>
        {
            center, //Anchor Point
            center + (Vector3.forward) * 0.5f, //Control Point
            center + (Vector3.forward) * .5f, //Control Point
            center + Vector3.forward//Anchor Point
        };

        for (var i = 0; i < count - 2; i++)
        {
            AddSegment(center + Vector3.forward * (i + 2));
        }
    }

    public Vector3 this[int i] => points[i];

    public int NumPoints => points.Count;

    public int NumSegments => (points.Count - 4) / 3 + 1;

    public void AddSegment(Vector3 newPosition)
    {
        newPosition.y = 0;
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]); // Second control point for previous last Anchor
        points.Add((points[points.Count - 1] + newPosition) * .5f); // Control point for last Anchor
        points.Add(newPosition); // New Anchor (Which is last Anchor now) added
    }

    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

    public void MovePoint(int i, Vector3 pos)
    {
        Vector3 deltaMove = pos - points[i];
        points[i] = pos;

        if (i % 3 == 0)
        {
            if (i + 1 < points.Count)
            {
                points[i + 1] += deltaMove;
            }
            if (i - 1 >= 0)
            {
                points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
            int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                float dst = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                Vector3 dir = (points[anchorIndex] - pos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + dir * dst;
            }
        }
    }

}