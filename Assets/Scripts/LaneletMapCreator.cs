using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LaneletMapCreator : MonoBehaviour 
{
    public void AddLaneletMapDefault(Vector3 position = default)
    {
        var text = "CreateLaneletMapDefault";
        Utilities.WriteConsole<string>(ref text);
        int nodeCount = 5;

        LaneletMap laneletMap = new LaneletMap();
        laneletMap.AddLaneletDefault();
    }
    
    public void AddLaneletMap()
    {
        var text = "CreateLaneletMap";
        Utilities.WriteConsole<string>(ref text);
    }
        
    public void RemoveLaneletMap()
    {
        var text = "RemoveLaneletMap";
        Utilities.WriteConsole<string>(ref text);
    }
}