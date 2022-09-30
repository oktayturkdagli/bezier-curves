using System.Collections.Generic;

namespace LaneletProject
{
    public interface IElement
    {
        int Id { get; set; }
        List<IElement> Owners { get; set; }
        
        public void Init();
        public void AddOwner(IElement newOwner);
        public void RemoveOwner(IElement Owner);
    }
}