using System;

public interface ITrapManager
{
    public bool OnTrapPlaced(Guid trapInventoryID, out TimedTextHelperConfigSO textHelper);
    public void OnTrapRemoved();
    public TextHelperConfigSO LookedAt();
}
