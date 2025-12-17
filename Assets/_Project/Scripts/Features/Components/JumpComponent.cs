using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using System;
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
    private GroundedCondition<GameObject> _groundedCondition;

    public void Initialize()
    {
        _layerFilter = new(_interactionMasks);
        _updateObstaclesAction = new(_layerFilter, _contactsObstacles);

        _groundedCondition = new(_enabled, _contactsObstacles);
        _groundCollider.Initialization(_updateObstaclesAction);

        _addForceAction = new(_rigidbody, _force, _impactForce);
        _jumpAction.Initialize(_groundedCondition, _addForceAction, onJump);
    }

    public void Enable()
    {
        _groundedCondition.Enable();
    }

    public void Disable()
    {
        _groundedCondition.Disable();
        _isGrounded.Dispose();
        onJump.Dispose();
    }
}

//    [Serializable]
//public class JumpComponent
//{
//    [SerializeField, Title("Behaviour")]
//    private AtomicVariable<bool> _enabled;

//    [Space, Title("Settings")]
//    [SerializeField]
//    private AtomicVariable<float> _impactForce;
//    [SerializeField]
//    private AtomicValue<Vector2> _force = new(Vector2.up);
//    [SerializeField]
//    private LayerMask _interactionMasks;

//    [Space, Title("References")]
//    [SerializeField]
//    private TriggerColliderDispatcher _groundCollider;
//    [SerializeField]
//    private Rigidbody2D _rigidbody;

//    [Space, Title("Dynamic state")]
//    [ShowInInspector, ReadOnly, HideInEditorMode]
//    private AtomicVariable<int> _contactsObstacles = new(0);
//    [SerializeField]
//    private AtomicVariable<bool> _isGrounded;
//    [ShowInInspector, ReadOnly]
//    private AtomicFunction<bool> _canJump = new();
//    public AtomicEvent onJump = new();
//    public IAtomicObservable<bool> IsGroundedObservable => _isGrounded;
//    public IAtomicValue<bool> IsGrounded => _isGrounded;
//    public IAtomicAction Jump => _jumpAction;
//    public IAtomicValue<bool> CanJump => _canJump;
//    public Rigidbody2D Rigidbody => _rigidbody;

//    [SerializeField]
//    [Get(GameConstants.Actions.Jump)]
//    private JumpAction _jumpAction;
//    private ObstacleCheckMechanics _groundMechanics;
//    private AddForceRigidBodyAction _addForceAction;

//    public void Initialize()
//    {
//        //condition
//        _groundMechanics = new(_contactsObstacles, _isGrounded, _groundCollider, _interactionMasks);
//        _canJump.Compose(() => _enabled.Value && _isGrounded.Value);

//        //jump
//        _addForceAction = new(_rigidbody, _force, _impactForce);
//        _jumpAction.Initialize(_canJump, _addForceAction, onJump);
//    }

//    public void Enable()
//    {
//        _groundMechanics.Enable();
//    }

//    public void Disable()
//    {
//        _groundMechanics.Dispose();
//        _isGrounded.Dispose();
//        onJump.Dispose();
//    }
//}
