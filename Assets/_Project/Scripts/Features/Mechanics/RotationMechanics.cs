using Atomic.Elements;
using UnityEngine;

public class RotationMechanics
{
    private readonly Transform _transform;
    private readonly IAtomicObservable<float> _moveDirectionObservable;
    private readonly IAtomicValue<bool> _enabled;
    private readonly IAtomicFunction<bool> _facingRightCondition;

    public RotationMechanics(
        IAtomicValue<bool> canRotate,
        IAtomicObservable<float> moveDirectionObservable,
        Transform transform,
        IAtomicFunction<bool> facingRightCondition)
    {
        _moveDirectionObservable = moveDirectionObservable;
        _transform = transform;
        _enabled = canRotate;
        _facingRightCondition = facingRightCondition;
    }

    public void Initialize()
    {
        _moveDirectionObservable.Subscribe(OnDirectionChanged);
    }

    public void Dispose()
    {
        _moveDirectionObservable.Unsubscribe(OnDirectionChanged);
    }

    private void OnDirectionChanged(float direction)
    {
        if (!_enabled.Value) return;

        bool facingRight = _facingRightCondition.Invoke();

        if ((direction < 0 && facingRight) || (direction > 0 && !facingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        bool facingRight = _facingRightCondition.Invoke();
        Vector3 scale = _transform.localScale;
        scale.x = facingRight ? -1f : 1f;
        _transform.localScale = scale;
    }
}
