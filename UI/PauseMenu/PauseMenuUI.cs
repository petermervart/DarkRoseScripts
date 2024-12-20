using UnityEngine;
using AYellowpaper;

public class PauseMenuUI : SmoothUICanvasGroup
{
    [SerializeField]
    private InterfaceReference<ISmoothUICanvasGroup> _pauseMenuButtons;

    [SerializeField]
    private InterfaceReference<ISmoothUICanvasGroup> _optionsButtons;

    public void OnOpenOptions()
    {
        _pauseMenuButtons.Value.Close();
        _optionsButtons.Value.Open();
    }

    public void OnCloseOptions()
    {
        _optionsButtons.Value.Close();
        _pauseMenuButtons.Value.Open();
    }
}
