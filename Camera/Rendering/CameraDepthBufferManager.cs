using UnityEngine;

public class CameraDepthBufferManager : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    // just for now... wrote custom mini SRP (depth map only for depth map collisions) for this camera but it cant be passed to VFX graph as texture because they use camera buffers directly.
    // Unity's reason is platform compatibility where texture can be different type... Solution for now is somehow pass depth buffer of this camera with the custom srp render into VFX graph but did not find solution yet.
    // Writing custom depth map collisions node might fix it.
    public void OnRender()

    {
        targetCamera.Render();
    }
}