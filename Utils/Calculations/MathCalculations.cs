using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;

public static class MathCalculations
{
    public static float GetDistance2D(Vector3 start, Vector3 target)
    {
        Vector3 vecToTarget = start - target;
        vecToTarget.y = 0f;

        return vecToTarget.magnitude;
    }

    public static float GetDistance3D(Vector3 start, Vector3 target)
    {
        Vector3 vecToTarget = start - target;

        return vecToTarget.magnitude;
    }
}
