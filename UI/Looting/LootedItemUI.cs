using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LootedItemUI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _fadeSpeed = 0.5f;

    [SerializeField]
    private TextMeshProUGUI _showText;

    [SerializeField]
    private Image _showIcon;

    public event Action<LootedItemUI> OnDestroyed;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        MoveAndFade();
    }

    public void SetTextAndIcon(string text, Sprite sprite)
    {
        _showText.SetText(text);
        _showIcon.sprite = sprite;
    }

    private void MoveAndFade()
    {
        _rectTransform.anchoredPosition += Vector2.up * _speed * Time.deltaTime;

        _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;

        if (_rectTransform.anchoredPosition.y > Screen.height)
        {
            HandleDestroy();
        }
    }

    private void HandleDestroy()
    {
        _canvasGroup.alpha = 1.0f;
        OnDestroyed?.Invoke(this);
    }
}
