using Atomic.Behaviours;
using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class RotationComponent
{
    private const float RIGHT_Y_EULER = 0f;
    private const float LEFT_Y_EULER = 180f;
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
        _facingRightCondition = new(_transfrom, _facingRight, RIGHT_Y_EULER);
        _rotationMechanics = new(_enabled, moveXDirectionObservable, _transfrom, _facingRightCondition, RIGHT_Y_EULER, LEFT_Y_EULER);
        _rotationMechanics.Initialize();
    }


    public void Dispose()
    {
        _rotationMechanics.Dispose();
    }
}