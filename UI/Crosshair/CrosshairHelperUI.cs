using System.Collections;
using UnityEngine;
using TMPro;

public class CrosshairHelperUI : SmoothUICanvasGroup
{
    [SerializeField]
    private TextMeshProUGUI _helperText;

    private bool _timedTextShowing = false;

    private Coroutine _timerCoroutine;

    private void SetText(string text, Color color)
    {
        _helperText.SetText(text);
        _helperText.color = color;
    }

    public void SetHelperText(string text, Color color)
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }

        Open();

        SetText(text, color);
    }

    public void SetTimedHelperText(string text, Color color, float duration)
    {
        Open(); // Ensure the UI is visible

        SetText(text, color);

        // Stop the current coroutine if it's running
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }

        // Start a new coroutine to clear the text after the duration
        _timerCoroutine = StartCoroutine(ClearHelperTextAfterDuration(duration));
    }

    private IEnumerator ClearHelperTextAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        _timerCoroutine = null;

        Close();
    }

    public override void OnFadedOut()
    {
        base.OnFadedOut();

        _helperText.SetText("");
    }
}