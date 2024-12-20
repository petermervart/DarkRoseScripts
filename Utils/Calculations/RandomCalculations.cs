using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;

public static class RandomCalculations
{
    public static float GetRandomNumberInRanges(float minRange1, float maxRange1, float minRange2, float maxRange2)
    {
        if (Random.Range(0, 2) == 0)
        {
            return Random.Range(minRange1, maxRange1);
        }
        else
        {
            return Random.Range(minRange2, maxRange2);
        }
    }

    private static int GetRandomNumberFromWeights(List<int> weights)
    {
        int _totalWeight = 0;

        foreach (int weight in weights)
        {
            _totalWeight += weight;
        }

        return Random.Range(0, _totalWeight);
    }

    public static int RandomIndexFromWeights(List<int> weights)
    {
        if (weights == null || weights.Count == 0)
        {
            DebugUtils.LogWarning("The weights list was empty");
            return -1;
        }

        int _randomValue = GetRandomNumberFromWeights(weights);
        int _sum = 0;

        for (int i = 0; i < weights.Count; i++)
        {
            _sum += weights[i];

            if (_randomValue < _sum)
            {
                return i;
            }
        }

        // This should not happen, but just in case
        return weights.Count - 1;
    }

    public static Vector3 GetRandomPointOnNavMeshBorder(Vector3 startPosition, Vector3 navMeshMaxDistances, Vector3 minDistanceFromPlayer, NavMeshSurface surface, int navMeshMask, float maxDistanceFromRandomStartPoint)
    {
        Vector3 randomOffset;

        if (Random.Range(0, 2) == 0)
        {
            randomOffset = new Vector3(
            GetRandomNumberInRanges(-navMeshMaxDistances.x, -minDistanceFromPlayer.x, minDistanceFromPlayer.x, navMeshMaxDistances.x),
            GetRandomNumberInRanges(-navMeshMaxDistances.y, -minDistanceFromPlayer.y, minDistanceFromPlayer.y, navMeshMaxDistances.y),
            Random.Range(-navMeshMaxDistances.z, navMeshMaxDistances.z)
        );
        }
        else
        {
            randomOffset = new Vector3(
            Random.Range(-navMeshMaxDistances.x, navMeshMaxDistances.x),
            GetRandomNumberInRanges(-navMeshMaxDistances.y, -minDistanceFromPlayer.y, minDistanceFromPlayer.y, navMeshMaxDistances.y),
            GetRandomNumberInRanges(-navMeshMaxDistances.z, -minDistanceFromPlayer.z, minDistanceFromPlayer.z, navMeshMaxDistances.z)
        );
        }

        Vector3 randomPoint = startPosition + randomOffset;

        NavMeshQueryFilter filter = new() { agentTypeID = surface.agentTypeID, areaMask = navMeshMask };  // Build query with the agent ID and mask

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, maxDistanceFromRandomStartPoint, filter))  // Get point on NavMesh
        {
            return hit.position;
        }

        DebugUtils.LogWarning("Failed To Get Point On NavMesh");

        return randomPoint;
    }
}
