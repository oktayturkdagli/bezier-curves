using UnityEngine;

public class PathPlacer : MonoBehaviour 
{
	public float spacing = .1f;
    public float resolution = 1;
	
    //This class is created to add objects on the bezier curve at runtime.
    private void Start () 
	{
		for (int i = 0; i < 2; i++)
		{
			var points = FindObjectOfType<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution, (PointType)i);
			foreach (var p in points)
			{
				var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				g.transform.position = p;
				g.transform.localScale = Vector3.one * spacing * .5f;
			}
		}
	}
}
