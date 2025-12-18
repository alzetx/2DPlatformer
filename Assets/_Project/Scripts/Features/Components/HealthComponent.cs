using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;
using static UnityEngine.Rendering.STP;

[Serializable, Is(GameConstants.ObjectTypes.Damageable)]
public class HealthComponent
{
    [Get(GameConstants.Variables.Health)]
    public HealthData HealthData;
    public RestoreHealthAction RestoreHealthAction;
    public AtomicEvent<int> TakeDamageEvent;
    public AtomicEvent OnDeath;
    private AtomicFunction<bool> _isAlive = new();
    [SerializeField, Get(GameConstants.Actions.Kill)]
    public IAtomicAction Kill => _killAction;

    private TakeDamageMechanics _takeDamageMechanics;
    [SerializeField]
    private KillAction _killAction;

    public IAtomicValue<bool> IsAlive => _isAlive;
    public void Initialize(CharacterConfig config)
    {
        HealthData.Initialize(config.MaxHP);
        _isAlive.Compose(() => HealthData.IsAlive.Value);
        _takeDamageMechanics = new(TakeDamageEvent, HealthData);
        RestoreHealthAction = new(HealthData);
        _killAction.Initialize(HealthData.IsAlive, HealthData.KillAction, OnDeath);

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
        HealthData?.Dispose();
    }
}
