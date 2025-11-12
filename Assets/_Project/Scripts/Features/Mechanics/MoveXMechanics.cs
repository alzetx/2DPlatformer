using Atomic.Elements;
using UnityEngine;

public class MoveXMechanics
{
    private readonly IAtomicValue<bool> _canMove;
    private readonly IAtomicValue<float> _moveDirection;
    private readonly IAtomicValue<float> _moveSpeed;
    private readonly IAtomicVariable<bool> _isMove;
    private readonly Rigidbody2D _rigidbody;

    public MoveXMechanics(IAtomicValue<bool> canMove, IAtomicVariable<float> moveDirection,
        IAtomicValue<float> moveSpeed, Rigidbody2D rigidbody, IAtomicVariable<bool> isMove)
    {
        _canMove = canMove;
        _moveDirection = moveDirection;
        _moveSpeed = moveSpeed;
        _rigidbody = rigidbody;
        _isMove = isMove;
    }

    public void FixedUpdate(float deltaTime)
    {
        if (_canMove.Value)
        {
            _isMove.Value = _moveDirection.Value != 0f;
            _rigidbody.linearVelocity = new Vector2((_moveDirection.Value * _moveSpeed.Value) * deltaTime, _rigidbody.linearVelocity.y);
        }
    }
}
