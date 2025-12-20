using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class HealthData : IntStorage
{
    [SerializeField]
    private AtomicVariable<bool> _isAlive;
    [SerializeField] 
    private AtomicVariable<bool> _isHealthFull;

    private AtomicAction _killAction;
    private AtomicEvent _onDeathEvent = new();

    public IAtomicObservable<bool> IsAliveObservable => _isAlive;
    public IAtomicValue<bool> IsAlive => _isAlive;
    public IAtomicValue<bool> IsHealthFull => _isHealthFull;
    public IAtomicEvent OnDeathEvent => _onDeathEvent;
    public IAtomicAction KillAction => _killAction;

    public void Initialize(int maxHP)
    {
        _max.Value = maxHP;
        _killAction = new(Kill);
        UpdateFlags();
        Initialize();
    }

    protected override void OnAfterValueChanged(int previous, int current)
    {
        UpdateFlags();
    }
    private void UpdateFlags()
    {
        bool alive = _current.Value > 0;
        _isAlive.Value = alive;
        _isHealthFull.Value = _current.Value >= _max.Value;

        if (!alive)
            _onDeathEvent.Invoke();
    }

    public void Kill() => SetValue(0);

    public override void Dispose()
    {
        base.Dispose();
        _isAlive.Dispose();
        _isHealthFull.Dispose();
    }
}
