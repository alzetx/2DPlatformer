using Sirenix.OdinInspector;
using System;

[Serializable]
public class HealthData
{
    public int maxHP;
    public int currentHP;

    public bool IsHealthFull => currentHP >= maxHP;

    public event Action<int> OnHealthChanged;
    public event Action OnDeathEvent;

    private void SetHealth(int newValue)
    {
        newValue = Math.Clamp(newValue, 0, maxHP);

        if (currentHP == newValue)
            return;

        currentHP = newValue;
        OnHealthChanged?.Invoke(currentHP);

        if (currentHP == 0)
            OnDeathEvent?.Invoke();
    }

    [Button]
    public void SetDamage(int damage)
    {
        SetHealth(currentHP - damage);
    }

    [Button]
    public void RestoreHealth(int value)
    {
        SetHealth(currentHP + value);
    }
}
