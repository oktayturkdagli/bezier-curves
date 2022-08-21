using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Way
{
    private int id;
    private List<int> owners = new List<int>(); // Lanelet id's, which have this way
    private List<Node> nodes = new List<Node>();

    public int ID { get => id; set => id = value; }
    public List<int> Owners { get => owners; set => owners = value; }
    public List<Node> Nodes { get => nodes; set => nodes = value; }

    public Way(int owner)
    {
        id = NumberManager.WayId++;
        owners.Add(owner);
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
    
    public void AddNodeDefault(bool toTheLeft = true)
    {
        float offset = toTheLeft ? -2.5f : 2.5f;
        Vector3 position = new Vector3(offset, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            position.z = i;
            Node node = new Node(id,  position);
            nodes.Add(node);
        }
    }
    
    public void AddNode(Node node)
    {
        Node tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
        if (tempNode != null) nodes.Remove(tempNode);
        
        nodes.Add(node);
    }
    
    public void RemoveNode(Node node)
    {
        Node tempNode = nodes.FirstOrDefault(element => element.ID == node.ID);
        if (tempNode != null) nodes.Remove(tempNode);
        
        nodes.Remove(node);
    }

}