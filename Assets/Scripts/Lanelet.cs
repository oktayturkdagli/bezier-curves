using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lanelet
{
    private int id;
    private List<int> owners = new List<int>(); // Lanelet id's, which have this way

    public Lanelet(int id, int owner)
    {
        this.id = NumberManager.LaneletId++;
        owners.Add(owner);
    }

    private void AddOwner(int newOwner)
    {
        bool isAlreadyOnList = owners.Contains(newOwner);
        if (isAlreadyOnList)
            return;
        
        owners.Add(newOwner);
    }
    
}
