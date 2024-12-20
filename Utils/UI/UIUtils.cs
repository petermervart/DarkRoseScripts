using System;
using System.Collections;
using UnityEngine;

public static class UIUtils
{
    public static IEnumerator FadeIn(CanvasGroup canvasGroup, bool isInteractable, bool blocksRaycast, float duration, Action onComplete = null)
    {
        if (canvasGroup == null)
        {
            DebugUtils.LogError("CanvasGroup is null!");
            yield break;
        }

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float initialAlpha = canvasGroup.alpha; // Get the current alpha value
        float remainingAlpha = 1f - initialAlpha; // How much alpha needs to increase
        float adjustedDuration = duration * remainingAlpha; // Scale duration based on remaining alpha

        float elapsedTime = 0f;

        while (elapsedTime < adjustedDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use unscaledDeltaTime for real-time updates
            canvasGroup.alpha = Mathf.Lerp(initialAlpha, 1f, elapsedTime / adjustedDuration);
            yield return null; // Wait for the next frame
        }

        canvasGroup.alpha = 1f;

        if (isInteractable)
            canvasGroup.interactable = true;

        if (blocksRaycast)
            canvasGroup.blocksRaycasts = true;

        onComplete?.Invoke();
    }

    public static IEnumerator FadeOut(CanvasGroup canvasGroup, float duration, Action onComplete = null)
    {
        if (canvasGroup == null)
        {
            DebugUtils.LogError("CanvasGroup is null!");
            yield break;
        }

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float initialAlpha = canvasGroup.alpha; // Get the current alpha value
        float adjustedDuration = duration * initialAlpha; // Scale duration based on the current alpha

        float elapsedTime = 0f;

        while (elapsedTime < adjustedDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use unscaledDeltaTime for real-time updates
            canvasGroup.alpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / adjustedDuration);
            yield return null; // Wait for the next frame
        }

        canvasGroup.alpha = 0f;

        onComplete?.Invoke();
    }
}