using Atomic.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GroundCheckMechanics
{
    private IAtomicVariable<bool> _isGrounded;
    private IAtomicVariable<LinkedList<GameObject>> _obstacles;
    private TriggerColliderDispatcher _colliderDispatcher;
    private LayerMask[] _masks;

    public GroundCheckMechanics(IAtomicVariable<LinkedList<GameObject>> obstacles, IAtomicVariable<bool> isGrounded, TriggerColliderDispatcher colliderDispatcher, params LayerMask[] masks)
    {
        _obstacles = obstacles;
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
            _obstacles.Value.AddLast(collider.gameObject);
            OnObstaclesChanged();
        }
    }

    private void OnTriggerExit(Collider2D collider)
    {
        if (IsInLayerMask(collider) && _obstacles.Value.Contains(collider.gameObject)) //todo optimize?
        {
            _obstacles.Value.Remove(collider.gameObject);
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
        _isGrounded.Value = _obstacles.Value.Count > 0 ? true : false;
    }

}
