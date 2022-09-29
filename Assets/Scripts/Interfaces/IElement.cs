using System.Collections.Generic;

namespace LaneletProject
{
    public interface IElement
    {
        int Id { get; set; }
        List<int> Owners { get; set; } //TODO: convert owners laneletmap type
        
        public void AddOwner(int newOwner);
        public void RemoveOwner(int Owner);
    }
}