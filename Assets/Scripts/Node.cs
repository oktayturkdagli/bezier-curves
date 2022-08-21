using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Node
{
    private int id;
    private Vector3 position;
    private List<int> owners = new List<int>(); // Way id's, which have this node

    public int ID { get => id; set => id = value; }
    public Vector3 Position { get => position; set => position = value; }
    public List<int> Owners { get => owners; set => owners = value; }

    public Node(int owner, Vector3 position)
    {
        id = NumberManager.NodeId++;
        owners.Add(owner);
        this.position = position;
    }

    private void AddOwner(int newOwner)
    {
        bool isAlreadyOnList = owners.Contains(newOwner);
        if (isAlreadyOnList)
            return;
        
        owners.Add(newOwner);
    }
    
    private void RemoveOwner(int owner)
    {
        bool isOnList = owners.Contains(owner);
        if (!isOnList)
            return;

        owners.Remove(owner);
    }
    
}

