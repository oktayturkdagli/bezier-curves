using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Way
{
    private int id;
    private List<int> owners = new List<int>(); // Lanelet id's, which have this way
    
    public Way(int id, int owner)
    {
        this.id = NumberManager.WayId++;
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