using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneLoaderVisual : MonoBehaviour
{
    private SceneLoader _loader;
    private List<IDisposable> _disposables = new();
    [SerializeField]
    private TextMeshProUGUI _textDescription, _textProgress;

    private float prevValue = 0;

    [SerializeField]
    private float _animationDuration;
    private Tweener _tweener;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private GameObject _root;
    private Tween _dotsTween;
    private string _baseText;
    private int _dotCount;

    [Inject]
    private void Construct(SceneLoader model)
    {
        _loader = model;
    }

    private void OnEnable()
    {
        Bind(true);
        AnimatePoints();
    }

    private void AnimatePoints()
    {
        _dotsTween = DOTween.Sequence()
            .AppendCallback(() => 
            {
            _dotCount = (_dotCount % 3) + 1; _textDescription.text = _baseText + new string('.', _dotCount);  }) 
            .AppendInterval(0.7f) 
            .SetLoops(-1);
            }

    private void OnDisable()
    {
        Bind(false);
    }

    private void Bind(bool bind)
    {
        if (bind)
        {
            IDisposable disposable = _loader.NextStep.Skip(1).Subscribe(OnStepChanged);
            IDisposable disposable1 = _loader.TotalProgress.Subscribe(OnProgressChanged);
            _loader.OnStartLoading += Show;
            _disposables.Add(disposable);
            _disposables.Add(disposable1);
        }
        else
        {
            _loader.OnStartLoading -= Show;
            _dotsTween?.Kill();
            _tweener?.Kill();
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }
    }

    private void Show()
    {
        _root.SetActive(true);
    }

    private void OnProgressChanged(float newValue)
    {
        _tweener?.Kill();
        _tweener = DOVirtual.Float(prevValue, newValue, _animationDuration, value =>
        {
            _textProgress.text = Mathf.RoundToInt(value * 100).ToString() + "%";
            _slider.value = value;
        });
        prevValue = newValue;
    }

    private void OnStepChanged(LoadingStep step)
    {
        _baseText = step.Description;
    }


}
