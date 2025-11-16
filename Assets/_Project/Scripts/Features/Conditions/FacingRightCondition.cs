using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class FacingRightCondition : IAtomicFunction<bool>
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<bool> _facingRight;

    public FacingRightCondition(Transform transform, IAtomicVariable<bool> facingRight)
    {
        _transform = transform;
        _facingRight = facingRight;
    }

    private bool FacingRight => _transform.localScale.x > 0f;

    public bool Invoke()
    {
        _facingRight.Value = FacingRight;
        return _facingRight.Value;
    }
}
