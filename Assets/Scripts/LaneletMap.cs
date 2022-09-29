using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public class LaneletMap : Element
    {
        public List<Lanelet> Lanelets { get; set; }

        public LaneletMap()
        {
            Lanelets = new List<Lanelet>();
        }

        public void AddLanelet(Lanelet lanelet)
        {
            Lanelet tempLanelet = Lanelets.FirstOrDefault(element => element.Id == lanelet.Id);
            if (tempLanelet != null) Lanelets.Remove(tempLanelet);

            lanelet.AddOwner(this);
            Lanelets.Add(lanelet);
        }

        public void RemoveLanelet(Lanelet lanelet)
        {
            Lanelet tempLanelet = Lanelets.FirstOrDefault(element => element.Id == lanelet.Id);
            if (tempLanelet != null) Lanelets.Remove(tempLanelet);

            Lanelets.Remove(lanelet);
        }

    }
}
