using Atomic.Elements;
using Atomic.Objects;
using System;

[Serializable]
public class HealthComponent
{
    [Get(GameConstants.Variables.Health)]
    public HealthData HealthData;
    private AtomicFunction<bool> _isAlive = new();
    public RestoreHealthAction RestoreHealthAction;
    public AtomicEvent<int> TakeDamageEvent;

    private TakeDamageMechanics _takeDamageMechanics;

    public IAtomicValue<bool> IsAlive => _isAlive;
    public void Initialize()
    {
        _isAlive.Compose(() => HealthData.currentHP > 0);
        _takeDamageMechanics = new(TakeDamageEvent, HealthData);
        RestoreHealthAction = new(HealthData);

    }

    public void Enable()
    {
        _takeDamageMechanics.OnEnable();
    }

    public void Disable()
    {
        _takeDamageMechanics.OnDisable();
    }

    public void Dispose()
    {
        TakeDamageEvent?.Dispose();
    }
}
