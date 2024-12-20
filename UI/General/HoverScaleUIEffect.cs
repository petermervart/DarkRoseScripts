using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class HoverScaleUIEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scaling Settings")]

    [SerializeField]
    protected float scaleMultiplier = 1.1f;

    [SerializeField]
    protected float transitionDuration = 0.3f;

    protected RectTransform targetTransform;

    protected Vector3 originalScale;
    protected Vector3 targetScale;
    protected float transitionProgress = 0f;
    protected bool isScalingUp = false;
    protected bool _isLocked = false;
    protected bool isTransitioning = false;

    protected virtual void Awake()
    {
        targetTransform = GetComponent<RectTransform>();

        originalScale = targetTransform.localScale;
        targetScale = originalScale;
    }

    protected virtual void Update()
    {
        HandleScaleEffect();
    }

    public virtual void ChangeLock(bool isLocked)
    {
        _isLocked = isLocked;

        if (isLocked)
        {
            // Gradually reset to original scale
            targetScale = originalScale;
            isScalingUp = false;
            isTransitioning = true;
        }
    }

    public virtual void HandleScaleEffect()
    {
        if (!isTransitioning)
            return;

        // Update the transition progress based on direction
        transitionProgress += Time.deltaTime / transitionDuration * (isScalingUp ? 1 : -1);

        // Clamp the transition progress to the range [0, 1]
        transitionProgress = Mathf.Clamp01(transitionProgress);

        // Interpolate the scale based on the current progress
        targetTransform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, transitionProgress);

        // Stop transitioning if the progress reaches the start or end
        if (transitionProgress == 0f || transitionProgress == 1f)
        {
            isTransitioning = false;
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (_isLocked)
            return;

        isScalingUp = true;
        isTransitioning = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (_isLocked)
            return;

        isScalingUp = false;
        isTransitioning = true;
    }
}