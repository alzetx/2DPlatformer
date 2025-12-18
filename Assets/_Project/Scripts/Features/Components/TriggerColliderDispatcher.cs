using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerColliderDispatcher : AtomicObject
{
    [SerializeField]
    private Collider2D _collider;

    OnTriggerCollisionMechanics onTriggerCollisionMechanics;
    public void Initialize(IAtomicAction<bool, Collider2D> onTriggerCollision)
    {
        _collider.isTrigger = true;
        onTriggerCollisionMechanics = new(onTriggerCollision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerCollisionMechanics?.Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerCollisionMechanics?.Exit(collision);
    }
}