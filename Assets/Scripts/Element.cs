using System.Collections.Generic;

namespace LaneletProject
{
    public abstract class Element : IElement
    {
        public int Id { get; set; }
        public List<int> Owners { get; set; }

        public Element()
        {
            Id = IdManager.GlobalId++;
            Owners = new List<int>();
        }

        public virtual void AddOwner(int newOwner)
        {
            bool isAlreadyOnList = Owners.Contains(newOwner);
            if (isAlreadyOnList)
                return;
            
            Owners.Add(newOwner);
        }

        public virtual void RemoveOwner(int owner)
        {
            bool isOnList = Owners.Contains(owner);
            if (!isOnList)
                return;

            Owners.Remove(owner);
        }
    }
}