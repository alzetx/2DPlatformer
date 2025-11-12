using Atomic.Elements;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JumpComponent
{
    [SerializeField, Title("Behaviour")]
    private AtomicVariable<bool> _enabled;

    [Space, Title("Settings")]
    [SerializeField]
    private AtomicVariable<float> _impactForce;
    [SerializeField]
    private AtomicValue<Vector2> _force = new(Vector2.up);
    [SerializeField]
    private LayerMask _interactionMasks;

    [Space, Title("References")]
    [SerializeField]
    private TriggerColliderDispatcher _groundCollider;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [Space, Title("Dynamic state")]
    [ShowInInspector, ReadOnly, HideInEditorMode]
    private AtomicVariable<LinkedList<GameObject>> _obstacles = new(new LinkedList<GameObject>());
    [SerializeField]
    private AtomicVariable<bool> _isGrounded;
    [ShowInInspector, ReadOnly]
    private AtomicFunction<bool> _canJump = new();
    public AtomicEvent onJump = new();
    public IAtomicObservable<bool> IsGrounded => _isGrounded;
    public IAtomicAction Jump => _jumpAction;
    public IAtomicValue<bool> CanJump => _canJump;

    [ShowInInspector]
    private JumpAction _jumpAction;
    private GroundCheckMechanics _groundMechanics;
    private AddForceRigidBodyAction _addForceAction;

    public void Initialize()
    {
        //condition
        _groundMechanics = new(_obstacles, _isGrounded, _groundCollider, _interactionMasks);
        _canJump.Compose(() => _enabled.Value && _isGrounded.Value);

        //jump
        _addForceAction = new(_rigidbody, _force, _impactForce);
        _jumpAction = new(_canJump, _addForceAction, onJump);
    }

    public void Enable()
    {
        _groundMechanics.Enable();
    }

    public void Dispose()
    {
        _groundMechanics.Dispose();
        _isGrounded.Dispose();
        onJump.Dispose();
    }
}
