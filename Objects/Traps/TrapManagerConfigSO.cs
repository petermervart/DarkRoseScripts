using UnityEngine;

[CreateAssetMenu(menuName = "Traps/Trap Manager Config", fileName = "Trap Manager")]
public class TrapManagerConfigSO : ScriptableObject
{
    public TextHelperConfigSO AlreadyOccupied;
    public TextHelperConfigSO NotOccupied;

    public TimedTextHelperConfigSO TriedToPlaceOnOccupied;
    public TimedTextHelperConfigSO PlacedTrap;
    public TimedTextHelperConfigSO CantPlaceHere;
}