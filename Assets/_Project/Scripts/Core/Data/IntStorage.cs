using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public abstract class IntStorage : Storage<int>
{
    [SerializeField]
    protected AtomicValue<int> _min;
    [SerializeField]
    protected AtomicVariable<int> _max;
    [SerializeField]
    protected AtomicVariable<int> _current;

    public event ValueChangedHandler<int> OnValueChangedEvent;
    public override IAtomicValue<int> Current => _current;
    public IAtomicValue<int> Min => _min;
    public IAtomicValue<int> Max => _max;

    public void Initialize()
    {
        _current.Value = Math.Clamp(_current.Value, _min.Value, _max.Value);
    }
    public override void AddValue(int value)
        => SetValue(_current.Value + value);

    public override void SpendValue(int value)
        => SetValue(_current.Value - value);

    protected override void SetValue(int newValue)
    {
        newValue = Math.Clamp(newValue, _min.Value, _max.Value);

        int prev = _current.Value;
        if (prev == newValue)
        {
            return;
        }
        _current.Value = newValue;
        OnValueChangedEvent?.Invoke(prev, newValue);
        OnAfterValueChanged(prev, newValue);

        OnValueChanged();
    }
    protected virtual void OnAfterValueChanged(int previous, int current) { }
    protected virtual void OnValueChanged() { }

    public override void Dispose()
    {
        _current.Dispose();
        _max.Dispose();
    }
}
