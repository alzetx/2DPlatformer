using Atomic.Elements;
using System;
using UnityEngine;

public class GroundCheckMechanics
{
    private IAtomicVariable<bool> _isGrounded;
    private IAtomicVariable<int> _contactObstacles;
    private TriggerColliderDispatcher _colliderDispatcher;
    private LayerMask[] _masks;

    public GroundCheckMechanics(
        IAtomicVariable<int> contactObstacles,
        IAtomicVariable<bool> isGrounded,
        TriggerColliderDispatcher colliderDispatcher,
        params LayerMask[] masks)
    {
        _contactObstacles = contactObstacles;
        _isGrounded = isGrounded;
        _colliderDispatcher = colliderDispatcher;
        _masks = masks;
    }

    public void Enable()
    {
        _colliderDispatcher.TriggerEnteredEvent += OnTriggerEnter;
        _colliderDispatcher.TriggerExitedEvent += OnTriggerExit;
    }

    public void Dispose()
    {
        _colliderDispatcher.TriggerEnteredEvent -= OnTriggerEnter;
        _colliderDispatcher.TriggerExitedEvent -= OnTriggerExit;
    }

    private void OnTriggerEnter(Collider2D collider)
    {
        if (IsInLayerMask(collider))
        {
            _contactObstacles.Value++;
            OnObstaclesChanged();
        }
    }

    private void OnTriggerExit(Collider2D collider)
    {
        if (IsInLayerMask(collider))
        {
            _contactObstacles.Value = Math.Max(0, _contactObstacles.Value - 1);
            OnObstaclesChanged();
        }
    }

    private bool IsInLayerMask(Collider2D collider)
    {
        int layerBit = 1 << collider.gameObject.layer;
        foreach (var mask in _masks)
        {
            if ((mask.value & layerBit) != 0)
                return true;
        }
        return false;
    }

    private void OnObstaclesChanged()
    {
        _isGrounded.Value = _contactObstacles.Value > 0;
    }
}
