using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class LaneletMap 
{ 
    private int id;
    private List<Lanelet> lanelets = new List<Lanelet>();

    public int ID { get => id; set => id = value; }
    public List<Lanelet> Lanelet { get => lanelets; set => lanelets = value; }

    public LaneletMap()
    {
        id = NumberManager.LaneletMapId++;
    }
    
    public void AddLaneletDefault()
    {
        Lanelet lanelet = new Lanelet(id);
        lanelet.AddWayDefault();
        lanelets.Add(lanelet);
    }

    public void AddLanelet(Lanelet lanelet)
    {
        Lanelet tempLanelet = lanelets.FirstOrDefault(element => element.ID == lanelet.ID);
        if (tempLanelet != null) lanelets.Remove(tempLanelet);
        
        lanelets.Add(lanelet);
    }
    
    public void RemoveLanelet(Lanelet lanelet)
    {
        Lanelet tempLanelet = lanelets.FirstOrDefault(element => element.ID == lanelet.ID);
        if (tempLanelet != null) lanelets.Remove(tempLanelet);
        
        lanelets.Remove(lanelet);
    }

}
