using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneLoaderVisual : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private TextMeshProUGUI _textProgress;
    [SerializeField] private float _animationDuration = 0.3f;
    [SerializeField] private float _dotsInterval = 0.7f;

    private SceneLoader _loader;
    private Tweener _progressTween;
    private Sequence _dotsSequence;

    private float _prevValue;
    private string _baseText;
    private int _dotCount;

    [Inject]
    private void Construct(SceneLoader loader)
    {
        _loader = loader;
    }
    public void Initialize()
    {
        Bind(true);
        _slider.value = 0f;
        _textProgress.text = $"{Mathf.RoundToInt(0)}%";
    }

    public void Dispose()
    {
        Bind(false);
    }
    private void Bind(bool bind)
    {
        if (bind)
        {
            _loader.OnStartLoading += OnStartLoading;
            _loader.OnProgressChanged += OnProgressChanged;
            _loader.OnStepChanged += OnStepChanged;
            _loader.OnLoadingFinished += OnLoadingFinished;
        }
        else
        {
            _loader.OnStartLoading -= OnStartLoading;
            _loader.OnProgressChanged -= OnProgressChanged;
            _loader.OnStepChanged -= OnStepChanged;
            _loader.OnLoadingFinished -= OnLoadingFinished;

            _progressTween?.Kill();
            _dotsSequence?.Kill();
        }
    }

    private void OnStartLoading()
    {
        _root.SetActive(true);
        AnimateDots();
    }

    private void OnLoadingFinished()
    {
        _root.SetActive(false);
        _dotsSequence?.Kill();
    }

    private void OnProgressChanged(float newValue)
    {
        _progressTween?.Kill();
        Debug.Log(newValue);

        _progressTween = DOVirtual.Float(_prevValue, newValue, _animationDuration, value =>
        {
            _slider.value = value;
            _textProgress.text = $"{Mathf.RoundToInt(value * 100)}%";
        });

        _prevValue = newValue;
    }

    private void OnStepChanged(LoadingStep step)
    {
        _baseText = step.Description;
        _textDescription.text = _baseText;
    }

    private void AnimateDots()
    {
        _dotsSequence?.Kill();

        _dotsSequence = DOTween.Sequence()
            .AppendCallback(() =>
            {
                _dotCount = (_dotCount % 3) + 1;
                _textDescription.text = _baseText + new string('.', _dotCount);
            })
            .AppendInterval(_dotsInterval)
            .SetLoops(-1);
    }

    
}
