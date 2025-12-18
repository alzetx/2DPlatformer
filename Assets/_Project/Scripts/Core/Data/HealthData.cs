using Atomic.Elements;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class HealthData
{
    [SerializeField]
    private AtomicVariable<int> _maxHP;
    [SerializeField]
    private AtomicVariable<int> _currentHP;
    [SerializeField]
    private AtomicVariable<bool> _isAlive;
    [SerializeField]
    private AtomicVariable<bool> _isHealthFull;
    private AtomicAction _killAction;
    private AtomicEvent _onDeathEvent = new();

    public IAtomicValue<bool> IsHealthFull => _isHealthFull;
    public IAtomicValue<bool> IsAlive => _isAlive;
    public IAtomicObservable<bool> IsAliveObservable => _isAlive;
    public IAtomicValue<int> MaxHP => _maxHP;
    public IAtomicValue<int> CurrentHP => _currentHP;
    public IAtomicObservable<int> OnHealthChanged => _currentHP;
    public IAtomicEvent OnDeathEvent => _onDeathEvent;
    public IAtomicAction KillAction => _killAction;

    public void Initialize(int MaxHP)
    {
        _maxHP.Value = MaxHP;
        _killAction = new(Kill);
        UpdateHealthFull();
        UpdateIsAlive();
    }

    private void UpdateHealthFull() => _isHealthFull.Value = _currentHP.Value >= _maxHP.Value;

    public void Dispose()
    {
        _currentHP.Dispose();
        _isAlive.Dispose();
        _isHealthFull.Dispose();
        _maxHP.Dispose();
    }
    private void SetHealth(int newValue)
    {
        newValue = Math.Clamp(newValue, 0, _maxHP.Value);

        if (_currentHP.Value == newValue)
        {
            return;

        }

        _currentHP.Value = newValue;
        UpdateIsAlive();
        UpdateHealthFull();
    }

    private void UpdateIsAlive()
    {
        bool alive = _currentHP.Value > 0;
        _isAlive.Value = alive;

        if (!alive)
            _onDeathEvent?.Invoke();
    }

    [Button]
    public void SetDamage(int damage)
    {
        SetHealth(_currentHP.Value - damage);
    }

    [Button]
    public void Kill()
    {
        SetHealth(0);
    }

    [Button]
    public void RestoreHealth(int value)
    {
        SetHealth(_currentHP.Value + value);
    }
}
