using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AYellowpaper;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image _foregroundHealthBar;

    [SerializeField]
    private Image _backgroundHealthBar;

    [SerializeField]
    private InterfaceReference<IHealth> _health;

    [SerializeField]
    private float _chunkTransitionTime = 0.2f;

    [SerializeField]
    private float _smoothDelay = 0.5f;

    [SerializeField]
    private float _backgroundTransitionTime = 2f;

    private Coroutine _updateCoroutine;

    public void Start()
    {
        _health.Value.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(float targetFill)
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }

        _updateCoroutine = StartCoroutine(AnimateHealthBar(targetFill));
    }

    private IEnumerator AnimateHealthBar(float targetFill)
    {
        float foregroundStartFill = _foregroundHealthBar.fillAmount;
        float backgroundStartFill = _backgroundHealthBar.fillAmount;

        if (targetFill < foregroundStartFill) // Damage case
        {
            // Step 1: Foreground health bar transitions to the target immediately
            yield return SmoothTransition(_foregroundHealthBar, foregroundStartFill, targetFill, _chunkTransitionTime);

            // Step 2: Wait, then background health bar catches up
            yield return new WaitForSeconds(_smoothDelay);
            yield return SmoothTransition(_backgroundHealthBar, backgroundStartFill, targetFill, _backgroundTransitionTime);
        }
        else if (targetFill > foregroundStartFill) // Healing case
        {
            // Step 1: Background health bar transitions to the target immediately
            yield return SmoothTransition(_backgroundHealthBar, backgroundStartFill, targetFill, _chunkTransitionTime);

            // Step 2: Wait, then foreground health bar catches up
            yield return new WaitForSeconds(_smoothDelay);
            yield return SmoothTransition(_foregroundHealthBar, foregroundStartFill, targetFill, _backgroundTransitionTime);
        }
    }

    private IEnumerator SmoothTransition(Image bar, float startFill, float targetFill, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / duration);
            yield return null;
        }

        bar.fillAmount = targetFill;
    }
}