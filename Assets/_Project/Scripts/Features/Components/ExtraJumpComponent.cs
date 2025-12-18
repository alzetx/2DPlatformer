using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class ExtraJumpComponent
{
    [SerializeField, Title("Behaviour")]
    public AtomicVariable<bool> Behaviour;

    [Space, Title("Settings")]
    [SerializeField]
    private AtomicVariable<float> _impactForce;
    [SerializeField]
    private AtomicValue<Vector2> _force = new(Vector2.up);
    [SerializeField]
    private LayerMask _interactionMasks;
    [SerializeField]
    private AtomicVariable<int> _maxCountJump;
    [SerializeField]
    private AtomicVariable<int> _usedCountJump;

    [Space, Title("References")]
    [SerializeField]
    private TriggerColliderDispatcher _groundCollider;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [Space, Title("Dynamic state")]
    [ShowInInspector, ReadOnly, HideInEditorMode]
    private AtomicCollection<GameObject> _contactsObstacles = new();
    [SerializeField]
    private AtomicVariable<bool> _isGrounded;
    public AtomicEvent onJump = new();
    public IAtomicObservable<bool> IsGroundedObservable => _isGrounded;
    public IAtomicValue<bool> IsGrounded => _isGrounded;
    [Get(GameConstants.Actions.Jump)]
    public IAtomicAction Jump => _jumpAction;
    public Rigidbody2D Rigidbody => _rigidbody;

    [SerializeField]
    private JumpAction _jumpAction;
    private AddForceRigidBodyAction _addForceAction;
    private ContactSetUpdateAction _updateObstaclesAction;
    private LayerMaskFilter _layerFilter;
    private JumpCounter _jumpCounter;
    private GroundedCondition<GameObject> _groundedCondition;
    private JumpCountCondition _jumpCountCondition;
    private JumpPermissionCondition _jumpPermissionCondition;

    public void Initialize(CharacterConfig config)
    {
        _maxCountJump.Value = config.MaxCountJump;
        _impactForce.Value = config.JumpForce;

        _jumpCounter = new(onJump, _isGrounded, _usedCountJump);
        _layerFilter = new(_interactionMasks);
        _updateObstaclesAction = new(_layerFilter, _contactsObstacles);

        _groundedCondition = new(_contactsObstacles, _isGrounded);
        _jumpCountCondition = new(_maxCountJump, _usedCountJump);
        _jumpPermissionCondition = new(Behaviour, _groundedCondition, _jumpCountCondition);


        _groundCollider.Initialize(_updateObstaclesAction);

        _addForceAction = new(_rigidbody, _force, _impactForce);
        _jumpAction.Initialize(_addForceAction, onJump, _jumpPermissionCondition, Behaviour);
    }

    public void Enable()
    {
        _groundedCondition.Enable();
        _jumpCounter.Enable();
    }

    public void Disable()
    {
        _groundedCondition.Disable();
        _jumpCounter.Disable();
    }

    public void Dispose()
    {
        Behaviour.Dispose();
        _isGrounded.Dispose();
        onJump.Dispose();
        _maxCountJump.Dispose();
        _usedCountJump.Dispose();
    }
}
