using Atomic.Elements;
using System;
using UnityEngine;

[Serializable]
public class AddForceRigidBodyAction : IAtomicAction
{
    private Rigidbody2D _rigidbody;
    private IAtomicValue<Vector2> _force;
    private IAtomicValue<float> _impactForce;

    public AddForceRigidBodyAction(Rigidbody2D rigidbody, IAtomicValue<Vector2> force, IAtomicValue<float> impactForce)
    {
        _rigidbody = rigidbody;
        _force = force;
        _impactForce = impactForce;
    }

    public void Invoke()
    {
        AddForce();
    }

    private void AddForce()
    {
        _rigidbody.AddForce(_force.Value * _impactForce.Value, ForceMode2D.Impulse);
    }
}