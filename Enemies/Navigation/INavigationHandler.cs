using System;
using UnityEngine;

public interface INavigationHandler
{
    void SetTarget(Transform target);
    bool CheckIfTargetInDistance(Vector3 position, float minDistance);
    void UpdateFollowing();
    void Resume();
    void Stop();
    void FaceTarget();
}
