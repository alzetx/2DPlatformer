using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

[Serializable]
public class MoveXComponent
{
    [SerializeField]
    public AtomicVariable<float> xDirection;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private AtomicVariable<bool> _enabled;
    [SerializeField]
    private AtomicVariable<bool> _isMoving;
    [SerializeField]
    private AtomicVariable<float> _moveSpeed;



    private MoveXMechanics _moveXMechanics;

    public IAtomicValue<bool> CanMove => _enabled;
    public Rigidbody2D Rigidbody2D => _rigidbody;


    public void Initialize()
    {
        _moveXMechanics = new(_enabled, xDirection, _moveSpeed, _rigidbody, _isMoving);
    }

    public void FixedTick(float deltaTime)
    {
        _moveXMechanics.FixedUpdate(deltaTime);
    }

    public void Dispose()
    {
        _enabled?.Dispose();
        xDirection?.Dispose();
        _isMoving?.Dispose();
        _moveSpeed?.Dispose();
    }
}
