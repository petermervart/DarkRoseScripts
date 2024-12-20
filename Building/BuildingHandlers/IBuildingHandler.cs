using System;

public interface IBuildingHandler
{
    public event Action<bool> OnBuildingOpenChanged;

    public event Action<BuildableItemConfigSO> OnBuildableChosen;

    public event Action<BuildableItemConfigSO> OnBuildablePlaced;

    public void OnBuild(ITrapManager trapManager);

    void HandleLock(bool isLocked);
}

