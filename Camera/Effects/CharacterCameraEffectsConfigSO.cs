using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Effects Config", fileName = "CameraEffectsConfig")]
public class CharacterCameraEffectsConfigSO : ScriptableObject
{
    [Header("FOV")]
    public float WalkingFOV = 60f;
    public float RunningFOV = 80f;
    public float FOVRate = 30f;
}
