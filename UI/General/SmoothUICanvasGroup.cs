using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]
public class SmoothUICanvasGroup : MonoBehaviour, ISmoothUICanvasGroup
{
    [SerializeField]
    private float _fadeTime;
    [SerializeField]
    private bool _shouldBlockRaycast;
    [SerializeField]
    private bool _isInteractable;

    private CanvasGroup _canvasGroup;

    private bool _isLocked;
    private Coroutine _fadeInCoroutine;
    private Coroutine _fadeOutCoroutine;

    private List<ISmoothUICanvasGroup> _childCanvasGroups = new List<ISmoothUICanvasGroup>();

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.ignoreParentGroups = true;

        FindChildCanvasGroups(transform);
    }

    private void FindChildCanvasGroups(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform childTransform = parent.GetChild(i);

            // Try to get ISmoothUICanvasGroup on this child
            ISmoothUICanvasGroup childCanvasGroup = childTransform.GetComponent<ISmoothUICanvasGroup>();

            if (childCanvasGroup != null)
            {
                // If ISmoothUICanvasGroup found, add it and skip its children
                _childCanvasGroups.Add(childCanvasGroup);
            }
            else
            {
                // If no ISmoothUICanvasGroup, continue searching its children
                FindChildCanvasGroups(childTransform);
            }
        }
    }

    public void ChangeChildrenShouldIgnore(bool shouldIgnore)
    {
        for (int i = 0; i < _childCanvasGroups.Count; i++)
        {
            _childCanvasGroups[i].ChangedShouldIgnoreParent(shouldIgnore);
        }
    }

    public virtual void Open()
    {
        if (_isLocked)
            return;

        gameObject.SetActive(true);

        if (_fadeOutCoroutine != null)
            StopCoroutine(_fadeOutCoroutine);

        ChangeChildrenShouldIgnore(false);

        _fadeInCoroutine = StartCoroutine(UIUtils.FadeIn(_canvasGroup, _isInteractable, _shouldBlockRaycast, _fadeTime, OnFadeIn));
    }

    public virtual void Close()
    {
        if (_isLocked || !gameObject.activeSelf)
            return;

        ChangeChildrenShouldIgnore(false);

        if (_fadeInCoroutine != null)
            StopCoroutine(_fadeInCoroutine);

        _fadeOutCoroutine = StartCoroutine(UIUtils.FadeOut(_canvasGroup, _fadeTime, OnFadedOut));
    }

    public virtual void LockElement()
    {
        _isLocked = true;
    }

    public virtual void UnlockElement()
    {
        _isLocked = false;
    }

    public virtual void OnFadedOut()
    {
        _fadeOutCoroutine = null;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public virtual void OnFadeIn()
    {
        _fadeInCoroutine = null;
        ChangeChildrenShouldIgnore(true);
    }

    public virtual void ChangedShouldIgnoreParent(bool shouldIgnoreParent)
    {
        _canvasGroup.ignoreParentGroups = shouldIgnoreParent;
    }
}