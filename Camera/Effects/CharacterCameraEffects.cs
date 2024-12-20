using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CharacterCameraEffects : MonoBehaviour, ICharacterCameraEffects
{
    [SerializeField] 
    private CharacterCameraEffectsConfigSO _cameraConfig;

    private CinemachineVirtualCamera _playerCamera;
    private float _currentFOV;

    private void Awake()
    {
        _playerCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        _currentFOV = _cameraConfig.WalkingFOV;
    }

    void Update()
    {
        if (_currentFOV != _playerCamera.m_Lens.FieldOfView)
        {
            _playerCamera.m_Lens.FieldOfView = Mathf.MoveTowards(_playerCamera.m_Lens.FieldOfView, _currentFOV, _cameraConfig.FOVRate * Time.deltaTime);
        }
    }

    public void ChangedRun(bool isRunning)
    {
        _currentFOV = isRunning ? _cameraConfig.RunningFOV : _cameraConfig.WalkingFOV;
    }
}
