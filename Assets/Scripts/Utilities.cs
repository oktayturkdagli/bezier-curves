using UnityEngine;

[ExecuteInEditMode]
public class Utilities : MonoBehaviour
{
    public static void WriteConsole<T>(ref T value)
    {
        print(value);
    }
    
}