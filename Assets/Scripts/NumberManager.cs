using UnityEngine;

namespace LaneletProject
{
    public class NumberManager
    {
        public static int NodeId { get; set; }

        public static int WayId { get; set; }

        public static int LaneletId { get; set; }

        public static int LaneletMapId { get; set; }

        public NumberManager(int nodeId = 0, int wayId = 0, int laneletId = 0, int laneletMapId = 0)
        {
            NodeId = nodeId;
            WayId = wayId;
            LaneletId = laneletId;
            LaneletMapId = laneletId;
        }

    }
}