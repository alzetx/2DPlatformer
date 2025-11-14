using Sirenix.OdinInspector;
using System;

[Serializable]
public class HealthData
{
    public int maxHP;
    public int currentHP;
    public bool IsHealthFull => currentHP >= maxHP;
    public event Action<int> OnHealthChanged;

    [Button]
    public void SetDamage(int damage)
    {
        currentHP = Math.Max(0, currentHP - damage);
        OnHealthChanged?.Invoke(currentHP);
    }

    [Button]
    public void RestoreHealth(int value)
    {
        currentHP = Math.Min(maxHP, currentHP + value);
        OnHealthChanged?.Invoke(currentHP);
    }
}
