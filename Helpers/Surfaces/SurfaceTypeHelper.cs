using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESurfaceType
{
    Ground = 0,
    Concrete = 1,
    Wood = 2
}

public class SurfaceTypeHelper : MonoBehaviour, ISurfaceTypeHelper
{
    [SerializeField] 
    private ESurfaceType _surfaceType;

    public ESurfaceType SurfaceType => _surfaceType;
}
