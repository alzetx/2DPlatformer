using System;
using UnityEngine;

public class HealthPresenter
{
    private readonly HealthView _view;
    private readonly HealthData _health;

    public HealthPresenter(HealthView view, HealthData health)
    {
        _view = view;
        _health = health;
    }

    public void Enable()
    {
        Bind(true);
    }

    public void Disable()
    {
        Bind(false);
    }

    private void Bind(bool bind)
    {
        if (bind)
        {
            _health.OnHealthChanged += OnHealthChanged;
        }
        else
        {
            _health.OnHealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(int currentHP)
    {
        _view.SetCurrentValue(currentHP);
    }
}
