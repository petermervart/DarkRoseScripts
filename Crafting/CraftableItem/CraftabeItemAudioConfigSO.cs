using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Craftable Item Audio Config", fileName = "CraftableItemAudioConfig")]
public class CraftableItemAudioConfigSO : ScriptableObject
{
    [Header("Crafting Audio")]
    public AudioClipHolder[] CraftingLoopSounds;
    public AudioClipHolder[] CraftedSounds;
}
