using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class SliderCurrencyView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeDuration = 0.4f;

    private Tween _valueTween;

    public void SetMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public void SetCurrentValue(float value)
    {
        StopAnimations();
        _slider.value = value;
    }

    public void ChangeValue(float current)
    {
        StopAnimations();

        _valueTween = DOTween.To(
                () => _slider.value,
                v => _slider.value = v,
                current,
                _changeDuration
            )
            .SetEase(Ease.OutCubic);
    }

    private void StopAnimations()
    {
        if (_valueTween == null)
            return;

        _valueTween.Kill();
        _valueTween = null;
    }
}