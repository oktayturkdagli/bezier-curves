using System;
using UnityEngine;

namespace LaneletProject
{
    [ExecuteInEditMode]
    public class UtilityManager : MonoBehaviour
    {
        private static Material anchorNodeMaterial;
        private static Material controlNodeMaterial;

        public static void LogMessage<T>(ref T message)
        {
            #if UNITY_EDITOR
            print(message);
            #endif
        }
        
        public static string NameChanger(int id, ElementTypes elementType)
        {
            var newName = id + " - " + elementType;
            return newName;
        }
        
        public static Material MaterialRequest(ElementTypes elementType)
        {
            switch (elementType)
            {
                case ElementTypes.LaneletMap:
                case ElementTypes.Lanelet:
                case ElementTypes.Way:
                case ElementTypes.AnchorNode:
                default:
                    if (anchorNodeMaterial) 
                        return anchorNodeMaterial;
                    anchorNodeMaterial = new Material(Shader.Find("Standard"));
                    anchorNodeMaterial.color = Color.blue;
                    return anchorNodeMaterial;
                case ElementTypes.ControlNode:
                    if (controlNodeMaterial) 
                        return controlNodeMaterial;
                    controlNodeMaterial = new Material(Shader.Find("Standard"));
                    controlNodeMaterial.color = Color.red;
                    return controlNodeMaterial;
            }
        }
    }
}