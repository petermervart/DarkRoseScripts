using UnityEngine;

[CreateAssetMenu(menuName = "Looting/Lootable Object Audio Config", fileName = "LootableObjectAudioConfig")]
public class LootableObjectAudioConfigSO : ScriptableObject
{
    [Header("Looting Audio")]
    public AudioClipHolder[] LootingLoopSounds;
    public AudioClipHolder[] LootedSound;
}
