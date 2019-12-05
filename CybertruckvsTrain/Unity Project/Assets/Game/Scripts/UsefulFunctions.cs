using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunctions
{
    public static void DebugRay(Vector3 origin, Vector3 direction, Color color)
    {
        Debug.DrawRay(origin, direction, color);
    }
}
