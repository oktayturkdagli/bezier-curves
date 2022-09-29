using System.Collections.Generic;

namespace LaneletProject
{
    public abstract class Element : IElement
    {
        public int Id { get; set; }
        public List<IElement> Owners { get; set; }

        public Element()
        {
            Id = IdManager.GlobalId++;
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