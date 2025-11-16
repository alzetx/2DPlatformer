using Atomic.Behaviours;
using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class RotationComponent
{
    [SerializeField]
    private Transform _transfrom;
    [SerializeField]
    private AtomicVariable<bool> _facingRight;
    [SerializeField]
    private AtomicVariable<bool> _enabled;

    private FacingRightCondition _facingRightCondition;
    private RotationMechanics _rotationMechanics;


    public void Initialize(IAtomicObservable<float> moveXDirectionObservable)
    {
        _facingRightCondition = new(_transfrom, _facingRight);
        _rotationMechanics = new(_enabled, moveXDirectionObservable, _transfrom, _facingRightCondition);
        _rotationMechanics.Initialize();
    }


    public void Dispose()
    {
        _rotationMechanics.Dispose();
    }
}