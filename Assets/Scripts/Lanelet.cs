using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Lanelet
{
    private int id;
    private List<int> owners = new List<int>(); // Lanelet id's, which have this way
    private List<Way> ways = new List<Way>();

    public int ID { get => id; set => id = value; }
    public List<int> Owners { get => owners; set => owners = value; }
    public List<Way> Ways { get => ways; set => ways = value; }

    public Lanelet()
    {
        id = NumberManager.LaneletId++;
    }
    
    public void AddOwner(int newOwner)
    {
        bool isAlreadyOnList = owners.Contains(newOwner);
        if (isAlreadyOnList)
            return;
        
        owners.Add(newOwner);
    }
    
    public void RemoveOwner(int owner)
    {
        bool isOnList = owners.Contains(owner);
        if (!isOnList)
            return;

        owners.Remove(owner);
    }
    
    public void AddWayDefault()
    {
        for (int i = 0; i < 2; i++)
        {
            Way way = new Way();
            way.AddNodeDefault();
            AddWay(way);
        }
    }
    
    public void AddWay(Way way)
    {
        Way tempWay = ways.FirstOrDefault(element => element.ID == way.ID);
        if (tempWay != null) ways.Remove(tempWay);
        
        way.AddOwner(id);
        ways.Add(way);
    }
    
    public void RemoveWay(Way way)
    {
        Way tempWay = ways.FirstOrDefault(element => element.ID == way.ID);
        if (tempWay == null) return;

        ways.Remove(way);
    }
    
}
