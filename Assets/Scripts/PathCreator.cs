using UnityEngine;

public class PathCreator : MonoBehaviour 
{
    [HideInInspector]
    public Path path;

    public void CreatePath(int distance, int count)
    {
        path = new Path(transform.position, distance, count);
    }
}
