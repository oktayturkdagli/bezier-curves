using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Node
{
    private int id;
    private Vector3 position;
    private List<int> owners = new List<int>(); // Way id's, which have this node

    public Node(int id, Vector3 position, int owner)
    {
        this.id = NumberManager.NodeId++;
        this.position = position;
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

