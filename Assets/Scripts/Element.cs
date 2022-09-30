using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public abstract class Element : MonoBehaviour, IElement
    {
        public int Id { get; set; }
        public ElementTypes Type { get; set; }
        public List<IElement> Owners { get; set; } = new List<IElement>();

        public virtual void Init()
        {
            Id = IdManager.Instance.GlobalId++;
            Owners = new List<IElement>();
        }
        
        public virtual void AddOwner(IElement newOwner)
        {
            bool isAlreadyOnList = Owners.Contains(newOwner);
            if (isAlreadyOnList)
                return;
            
            Owners.Add(newOwner);
        }
        
        public virtual void RemoveOwner(IElement owner)
        {
            bool isOnList = Owners.Contains(owner);
            if (!isOnList)
                return;

            Owners.Remove(owner);
        }
    }
}