using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

public class RotationMechanics
{
    private readonly float _rightYEuler;
    private readonly float _leftYEuler = 180f;
    private readonly Transform _transform;
    private readonly IAtomicObservable<float> _moveDirectionObservable;
    private readonly IAtomicValue<bool> _enabled;
    private IAtomicFunction<bool> _facingRightCondition;
    public RotationMechanics(IAtomicValue<bool> canRotate, IAtomicObservable<float> moveDirectionObservable, Transform transform, IAtomicFunction<bool> facingRightCondition, float rightYEuler, float  leftYEuler)
    {
        _moveDirectionObservable = moveDirectionObservable;
        _transform = transform;
        _enabled = canRotate;
        _facingRightCondition = facingRightCondition;
        _rightYEuler = rightYEuler;
        _leftYEuler = leftYEuler;
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
        bool facingRight = _facingRightCondition.Invoke();
        if (_enabled.Value && (direction < 0 && facingRight ||
            direction > 0 && !facingRight))
            Flip();
    }

    private void Flip()
    {
        var yEuler = _facingRightCondition.Value == true ? _leftYEuler : _rightYEuler;
        Vector3 direction = Vector3.up * yEuler;
        _transform.rotation = Quaternion.Euler(direction);
    }
}