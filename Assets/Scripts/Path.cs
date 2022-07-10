using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path 
{
    [SerializeField, HideInInspector] private List<Vector3> leftPoints;
    [SerializeField, HideInInspector] private List<Vector3> rightPoints;

    public Path(Vector3 center, int distance, int count)
    {
        var leftCenter = center + (Vector3.left * distance / 2);
        var rightCenter = center + (Vector3.right * distance / 2);
        leftPoints = CreatePoints(leftCenter, count);
        rightPoints = CreatePoints(rightCenter, count);
    }

    public void AddSegment(Vector3 newPosition, int distance)
    {
        var leftCenter = newPosition + (Vector3.left * (distance / 2));
        var rightCenter = newPosition + (Vector3.right * (distance / 2));
        AddPoint(leftCenter, PointType.LeftPoint);
        AddPoint(rightCenter, PointType.RightPoint);
    }

    public Vector3[] GetPointsInSegment(int i, PointType pointType)
    {
        return GetPoints(i, pointType);
    }

    public void MovePoint(int i, Vector3 pos, PointType pointType)
    {
        pos.y = 0;
        var points = pointType == PointType.LeftPoint ? leftPoints : rightPoints;

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

    public Vector3 Point(int i, PointType pointType)
    {
        return pointType == PointType.LeftPoint ? leftPoints[i] : rightPoints[i];
    }

    public int NumPoints(PointType pointType)
    {
        return pointType == PointType.LeftPoint ? leftPoints.Count : rightPoints.Count;
    }

    public int NumSegments(PointType pointType)
    {
        // Constructor segment has 4 point but we count as 1 and other anchors has 3 element (Itself and 2 control point)
        return pointType == PointType.LeftPoint ? leftPoints.Count/ 3 : rightPoints.Count/ 3;
    }

    private List<Vector3> CreatePoints(Vector3 center, int count)
    {
        center.y = 0;
        var points = new List<Vector3>
        {
            center, //Anchor Point
            center + (Vector3.forward) * 0.5f, //Control Point
            center + (Vector3.forward) * .5f, //Control Point
            center + Vector3.forward//Anchor Point
        };
        
        for (var i = 0; i < count - 2; i++) // Add other segments
        {
            Vector3 newPosition = center + Vector3.forward * (i + 2);
            points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);  // Second control point is adding for previous last Anchor
            points.Add((points[points.Count - 1] + newPosition) * .5f); // New control point is adding for last Anchor
            points.Add(newPosition); // New Anchor (Which is last Anchor now) is adding
        }
        
        return points;
    }
    
    private void AddPoint(Vector3 newPosition, PointType pointType)
    {
        var points = pointType == PointType.LeftPoint ? leftPoints : rightPoints;
        newPosition.y = 0;
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]); // Second control point is adding for previous last Anchor
        points.Add((points[points.Count - 1] + newPosition) * .5f); // New control point is adding for last Anchor
        points.Add(newPosition); // New Anchor (Which is last Anchor now) is adding
    }
    
    private Vector3[] GetPoints(int i, PointType pointType)
    {
        var points = leftPoints;
        if (pointType == PointType.RightPoint)
            points = rightPoints;
        
        return new[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }
}

[System.Serializable]
public enum PointType
{
    LeftPoint,
    RightPoint
}
