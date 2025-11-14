using Atomic.Elements;
using Atomic.Objects;
using System;

[Serializable]
public class HealthComponent
{
    [Get(Names.Variable.Health)]
    public HealthData HealthData;
    private AtomicFunction<bool> _isAlive = new();
    public RestoreHealthAction RestoreHealthAction;
    public AtomicEvent DeathEvent;
    public AtomicEvent<int> TakeDamageEvent;

    private TakeDamageMechanics _takeDamageMechanics;
    private DeathMechanics _deathMechanics;

    public IAtomicValue<bool> IsAlive => _isAlive;
    public void Initialize()
    {
        _isAlive.Compose(() => HealthData.currentHP > 0);
        _takeDamageMechanics = new(TakeDamageEvent, HealthData);
        _deathMechanics = new(HealthData, DeathEvent);
        RestoreHealthAction = new(HealthData);

    }

    public void Enable()
    {
        _takeDamageMechanics.OnEnable();
        _deathMechanics.OnEnable();
    }

    public void Disable()
    {
        _takeDamageMechanics.OnDisable();
        _deathMechanics.OnDisable();
    }

    public void Dispose()
    {
        TakeDamageEvent?.Dispose();
        DeathEvent?.Dispose();
    }
}
