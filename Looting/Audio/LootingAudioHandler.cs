using UnityEngine;
using AYellowpaper;

public class LootingAudioHandler : MonoBehaviour, ILootingAudioHandler
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private InterfaceReference<ILootingHandler> _lootingUIHandler;

    private void Start()
    {
        _lootingUIHandler.Value.OnLootingStateChanged += HandleObjectBeingLooted;
        _lootingUIHandler.Value.OnLootedItem += HandleObjectLooted;
    }

    public void HandleObjectLooted(LootableObjectConfigSO lootedObject)
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, lootedObject.AudioConfig.LootedSound);
    }

    public void HandleObjectBeingLooted(LootableObjectConfigSO lootedObject, bool isLooting)
    {
        AudioUtils.PlayClipOnLoop(isLooting, _audioSource, lootedObject.AudioConfig.LootingLoopSounds);
    }
}