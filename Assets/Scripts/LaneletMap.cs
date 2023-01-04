using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace LaneletProject
{
    [System.Serializable]
    public class LaneletMap : Element
    {
        public List<Lanelet> Lanelets { get; set; }
        
        public override void Init()
        {
            base.Init();
            Lanelets = new List<Lanelet>();
            Type = ElementTypes.LaneletMap;
        }
        
        public Lanelet AddLanelet(Vector3 position = default) //TODO: Make Generic pls
        {
            var text = "AddLanelet";
            UtilityManager.LogMessage<string>(ref text);
            
            GameObject newObj = new GameObject("New Object");
            newObj.transform.parent = transform;
            newObj.transform.localPosition = position;
            newObj.transform.localRotation = Quaternion.identity;
            Lanelet lanelet = newObj.AddComponent<Lanelet>();
            newObj.name = UtilityManager.NameChanger(lanelet.Id, ElementTypes.Lanelet);
            
            lanelet.Init();
            lanelet.AddOwner(this);
            Lanelets.Add(lanelet);
            return lanelet;
        }
        
        public void RemoveLanelet(Lanelet lanelet) //TODO: Change remove system
        {
            Lanelet tempLanelet = Lanelets.FirstOrDefault(element => element.Id == lanelet.Id);
            if (tempLanelet != null) Lanelets.Remove(tempLanelet);

            Lanelets.Remove(lanelet);
        }
    }
}
