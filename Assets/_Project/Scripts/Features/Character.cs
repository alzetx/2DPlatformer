using Atomic.Objects;
using Sirenix.OdinInspector;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

public class Character : AtomicObject, IGameStartListener, IGameFixedTickable, IGameTickable
{
    [SerializeField]
    private MoveXComponent _moveComponent;
    [SerializeField]
    private RotationComponent _rotationComponent;
    [SerializeField]
    private JumpComponent _jumpComponent;


    private IInput _input;
    [ShowInInspector, ReadOnly, HideInEditorMode]
    private Fsm _fsm;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    public void OnStartGame()
    {
        _moveComponent.Initialize();
        _rotationComponent.Initialize(_moveComponent.xDirection);
        _jumpComponent.Initialize();
        _jumpComponent.Enable();
        InitializeFSM();
    }

    private void InitializeFSM()
    {
        _fsm = new();
        IdleState idleState = new(_input, _moveComponent, _jumpComponent, _fsm);
        MoveState moveState = new(_input, _moveComponent, _jumpComponent, _fsm);
        JumpState jumpState = new(_jumpComponent, _moveComponent.Rigidbody2D, _fsm);
        FallState fallState = new(_jumpComponent, _moveComponent.Rigidbody2D, _fsm);
        //GroundState groundState = new(_input, _moveComponent, _jumpComponent, _fsm);
        //AirState airState = new(_jumpComponent, _moveComponent.Rigidbody2D, _fsm);
        _fsm.AddStates(idleState, moveState, jumpState, fallState);
        _fsm.SetState<IdleState>();
    }
    public void FixedTick(float deltaTime)
    {
        _moveComponent.FixedTick(deltaTime);
    }
    public void Tick(float deltaTime)
    {
        _fsm.Update();
    }

    private void OnDestroy()
    {
        _moveComponent.Dispose();
        _jumpComponent.Dispose();
    }

    
}
