using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaneletProject
{
    [System.Serializable]
    public class Lanelet : Element
    {
        public List<Way> Ways { get; set; }

        public override void Init()
        {
            base.Init();
            Ways = new List<Way>();
            Type = ElementTypes.Lanelet;
        }

        public Way AddWay(Vector3 position = default)
        {
            var text = "AddWay";
            UtilityManager.LogMessage<string>(ref text);
            
            GameObject newObj = new GameObject("New Object");
            newObj.transform.parent = transform;
            newObj.transform.localPosition = position;
            newObj.transform.localRotation = Quaternion.identity;
            Way way = newObj.AddComponent<Way>();
            newObj.name = UtilityManager.NameChanger(way.Id, ElementTypes.Way);
            
            way.Init();
            way.AddOwner(this);
            Ways.Add(way);
            return way;
        }

        public void RemoveWay(Way way)
        {
            Way tempWay = Ways.FirstOrDefault(element => element.Id == way.Id);
            if (tempWay == null) return;

            Ways.Remove(way);
        }

    }
}
