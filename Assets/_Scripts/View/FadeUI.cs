using UnityEngine;
using DG.Tweening;
using System;

public class FadeUI : Singleton<FadeUI>
{
    public Action OnFadeInFinish;
    [SerializeField] private bool isActiveOnStart;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    private bool _isVisible = false;
    private SpriteRenderer _sr;

    protected override void Awake()
    {
        base.Awake();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _isVisible = isActiveOnStart;
        if (isActiveOnStart)
        {
            _sr.color = startColor;
            Fade();
        }
        else
        {
            _sr.color = endColor;
        }
    }

    public void Fade()
    {
        if (_isVisible)
        {
            _sr.DOColor(endColor, fadeOutDuration).SetEase(Ease.InOutSine);
        }
        else
        {
            _sr.DOColor(startColor, fadeInDuration).SetEase(Ease.InOutSine);
            OnFadeInFinish?.Invoke();
        }
        _isVisible = !_isVisible;
    }
}
