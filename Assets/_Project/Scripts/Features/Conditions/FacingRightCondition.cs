using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class FacingRightCondition : IAtomicFunction<bool>
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<bool> _facingRight;
    public bool FacingRight => _transform.eulerAngles.y == _rightYEuler;
    private readonly float _rightYEuler = 0f;

    public FacingRightCondition(Transform transform, IAtomicVariable<bool> facingRight, float rightYEuler)
    {
        _transform = transform;
        _facingRight = facingRight;
        _rightYEuler = rightYEuler;
    }

    public bool Invoke()
    {
        _facingRight.Value = FacingRight;
        return _facingRight.Value;
    }
}