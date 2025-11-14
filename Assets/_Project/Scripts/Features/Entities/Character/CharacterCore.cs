using Atomic.Objects;
using System;
using UnityEngine;

[Serializable]
public class CharacterCore
{
    [Section]
    public MoveXComponent MoveComponent;
    [Section]
    public JumpComponent JumpComponent;

    [SerializeField]
    private RotationComponent _rotationComponent;


    public void OnStartGame()
    {
        MoveComponent.Initialize();
        _rotationComponent.Initialize(MoveComponent.xDirection);
        JumpComponent.Initialize();
        JumpComponent.Enable();
    }

    public void OnEnable()
    {
        JumpComponent.Enable();
    }

    public void OnDisable()
    {
        JumpComponent.Disable();
    }

    public void FixedTick(float deltaTime)
    {
        MoveComponent.FixedTick(deltaTime);
    }

    public void OnDestroy()
    {
        MoveComponent.Dispose();
        JumpComponent.Disable();
    }
}
