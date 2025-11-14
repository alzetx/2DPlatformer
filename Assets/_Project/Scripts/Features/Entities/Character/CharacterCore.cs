using Atomic.Objects;
using System;
using UnityEngine;

[Serializable]
public class CharacterCore
{
    [Section]
    public MoveXComponent _moveComponent;
    [SerializeField]
    private RotationComponent _rotationComponent;
    [SerializeField]
    private JumpComponent _jumpComponent;

    public void OnStartGame()
    {
        _moveComponent.Initialize();
        _rotationComponent.Initialize(_moveComponent.xDirection);
        _jumpComponent.Initialize();
        _jumpComponent.Enable();
    }

    public void OnEnable()
    {
        _jumpComponent.Enable();
    }

    public void OnDisable()
    {
        _jumpComponent.Disable();
    }

    public void FixedTick(float deltaTime)
    {
        _moveComponent.FixedTick(deltaTime);
    }

    public void OnDestroy()
    {
        _moveComponent.Dispose();
        _jumpComponent.Disable();
    }
}
