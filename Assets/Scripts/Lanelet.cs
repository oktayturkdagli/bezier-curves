using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class Lanelet : Element
    {
        public List<Way> Ways { get; set; }
        
        public Lanelet()
        {
            Ways = new List<Way>();
        }

        public void AddWay(Way way)
        {
            Way tempWay = Ways.FirstOrDefault(element => element.Id == way.Id);
            if (tempWay != null) Ways.Remove(tempWay);

            way.AddOwner(this);
            Ways.Add(way);
        }

        public void RemoveWay(Way way)
        {
            Way tempWay = Ways.FirstOrDefault(element => element.Id == way.Id);
            if (tempWay == null) return;

            Ways.Remove(way);
        }

    }
}
