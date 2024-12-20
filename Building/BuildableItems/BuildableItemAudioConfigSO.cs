using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Buildable Item Audio Config", fileName = "BuildableItemAudioConfig")]
public class BuildableItemAudioConfigSO : ScriptableObject
{
    [Header("Building Audio")]
    public AudioClipHolder[] PlacedItemSounds;
    public AudioClipHolder[] ChoseItemSounds;
}
