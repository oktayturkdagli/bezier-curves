namespace LaneletProject
{
    public class IdManager
    {
        public static int GlobalId { get; set; }
        
        public static int NodeAnchorId { get; set; }
        
        public static int NodeControlId { get; set; }

        public static int WayId { get; set; }

        public static int LaneletId { get; set; }

        public static int LaneletMapId { get; set; }

        public IdManager(int globalId = 0, int laneletMapId = 0, int laneletId = 0, int wayId = 0, int nodeControlId = 0, int nodeAnchorId = 0)
        {
            GlobalId = globalId;
            NodeAnchorId = nodeAnchorId;
            NodeControlId = nodeControlId;
            WayId = wayId;
            LaneletId = laneletId;
            LaneletMapId = laneletId;
        }
    }
}