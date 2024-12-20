
public interface ISmoothUICanvasGroup
{
    void LockElement();
    void UnlockElement();
    void Open();
    void Close();
    void OnFadedOut();
    void OnFadeIn();
    void ChangedShouldIgnoreParent(bool shouldIgnoreParent);
}
