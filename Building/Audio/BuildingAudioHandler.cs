using UnityEngine;
using AYellowpaper;

public class BuildingAudioHandler : MonoBehaviour, IBuildingAudioHandler
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private InterfaceReference<IBuildingHandler> _buildingHandler;

    private void Start()
    {
        _buildingHandler.Value.OnBuildableChosen += HandleChoseItem;
        _buildingHandler.Value.OnBuildablePlaced += HandleBuiltItem;
    }

    public void HandleBuiltItem(BuildableItemConfigSO builtItem)
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, builtItem.AudioConfig.PlacedItemSounds);
    }

    public void HandleChoseItem(BuildableItemConfigSO chosenItem)
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, chosenItem.AudioConfig.ChoseItemSounds);
    }
}