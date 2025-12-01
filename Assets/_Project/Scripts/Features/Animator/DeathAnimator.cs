using Atomic.Elements;
using System;
using UnityEngine;

public class DeathAnimator
{
    private Animator _animator;
    private HealthData _healthData;

    public DeathAnimator(Animator animator, HealthData healthData)
    {
        _animator = animator;
        _healthData = healthData;
    }

    public void Enable()
    {
        _healthData.OnDeathEvent += OnDeath;
    }

    public void Disable()
    {
        _healthData.OnDeathEvent -= OnDeath;
    }
    private void OnDeath()
    {
        _animator.SetTrigger(GameConstants.AnimatorKeys.Death);
    }
}
