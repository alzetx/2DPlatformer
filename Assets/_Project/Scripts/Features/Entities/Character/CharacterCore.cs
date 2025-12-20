using Atomic.Objects;
using System;
using UnityEngine;

[Serializable]
public class CharacterCore
{
    [SerializeField]
    private CharacterConfig _config;

    [Section]
    public MoveXComponent moveComponent;
    [Section]
    public ExtraJumpComponent jumpComponent;
    [Section]
    public HealthComponent healthComponent;

    public CoinComponent coinComponent;

    [SerializeField]
    private RotationComponent _rotationComponent;

    private MechanicsController _mechanicsController;


    public void OnStartGame()
    {
        _mechanicsController = 
            new(healthComponent.HealthData.IsAlive,
            healthComponent.HealthData.IsAliveObservable,
            moveComponent.Behaviour, jumpComponent.Behaviour,
            _rotationComponent.Behaviour);


        moveComponent.Initialize(_config);
        _rotationComponent.Initialize(moveComponent.xDirection);
        jumpComponent.Initialize(_config);
        healthComponent.Initialize(_config);
        coinComponent.Initialize();
    }


    public void OnEnable()
    {
        jumpComponent.Enable();
        healthComponent.Enable();
        _mechanicsController.Enable();
    }

    public void OnDisable()
    {
        jumpComponent.Disable();
        healthComponent.Disable();
        _mechanicsController.Disable();
    }

    public void FixedTick(float deltaTime)
    {
        moveComponent.FixedTick(deltaTime);
    }

    public void OnDestroy()
    {
        OnDisable();
        moveComponent.Dispose();
        healthComponent.Dispose();
    }
}
