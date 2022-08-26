using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LaneletProject
{
    [System.Serializable]
    public abstract class Node
    {
        private int id;
        private Vector3 position;
        private List<int> owners = new List<int>(); // Way id's, which have this node

        public int ID
        {
            get => id;
            set => id = value;
        }

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public List<int> Owners
        {
            get => owners;
            set => owners = value;
        }

        public Node(Vector3 position)
        {
            id = NumberManager.NodeId++;
            this.position = position;
        }

        public void AddOwner(int newOwner)
        {
            bool isAlreadyOnList = owners.Contains(newOwner);
            if (isAlreadyOnList)
                return;

            owners.Add(newOwner);
        }

        public void RemoveOwner(int owner)
        {
            bool isOnList = owners.Contains(owner);
            if (!isOnList)
                return;

            owners.Remove(owner);
        }

    }
}

