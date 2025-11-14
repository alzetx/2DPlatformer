using Atomic.Objects;
using System;
using UnityEngine;

[Serializable]
public class CharacterCore
{
    [Section]
    public MoveXComponent moveComponent;
    [Section]
    public JumpComponent jumpComponent;
    [Section]
    public HealthComponent healthComponent;

    [SerializeField]
    private RotationComponent _rotationComponent;


    public void OnStartGame()
    {
        moveComponent.Initialize();
        _rotationComponent.Initialize(moveComponent.xDirection);
        jumpComponent.Initialize();
        healthComponent.Initialize();
        jumpComponent.Enable();
    }

    public void OnEnable()
    {
        jumpComponent.Enable();
        healthComponent.Enable();
    }

    public void OnDisable()
    {
        jumpComponent.Disable();
        healthComponent.Disable();
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
