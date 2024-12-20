using UnityEngine;
using AYellowpaper;

public class AudioCraftingHandler : MonoBehaviour, IAudioCraftingHandler
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private InterfaceReference<ICraftingHandler> _craftingUIHandler;

    private void Start()
    {
        _craftingUIHandler.Value.OnItemCrafted += HandleItemCrafted;
        _craftingUIHandler.Value.OnCraftingStatusChanged += HandleItemBeingCrafted;
    }

    public void HandleItemCrafted(CraftableItemConfigSO craftedItem)
    {
        AudioUtils.PlayOneShotRandomAudioClip(_audioSource, craftedItem.AudioConfig.CraftedSounds);
    }

    public void HandleItemBeingCrafted(CraftableItemConfigSO craftedItem, bool isCrafting)
    {
        AudioUtils.PlayClipOnLoop(isCrafting, _audioSource, craftedItem.AudioConfig.CraftingLoopSounds);
    }
}