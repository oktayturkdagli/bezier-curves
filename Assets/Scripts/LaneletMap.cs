using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LaneletMap 
{ 
    private int id;

    public LaneletMap(int id, int owner)
    {
        this.id = NumberManager.LaneletMapId++;
    }

}
