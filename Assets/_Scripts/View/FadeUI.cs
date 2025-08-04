using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

/// <summary>
/// Responsible for fading in and out an image.
/// </summary>
public class FadeUI : Singleton<FadeUI>
{
    public Action OnEnableFinish;
    public float EnableDuration { get => enableDuration; }
    public float DisableDuration { get => disableDuration; }
    [SerializeField] private bool isActiveOnStart;
    [SerializeField] private float enableDuration;
    [SerializeField] private float disableDuration;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    private bool _isEnabled = true;
    private Image _image;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
    }

    void Start()
    {
            _image.color = startColor;
    }

    public void Fade()
    {
        if (_isEnabled)
        {
            _image.DOColor(endColor, disableDuration).SetEase(Ease.InOutSine);
        }
        else
        {
            _image.DOColor(startColor, enableDuration).OnComplete(() => OnEnableFinish?.Invoke())
            .SetEase(Ease.InOutSine);
        }
        _isEnabled = !_isEnabled;
    }
}
