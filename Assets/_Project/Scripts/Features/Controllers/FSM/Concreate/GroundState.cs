using System;

public class GroundState : FsmState
{
    protected readonly JumpComponent _jumpComponent;
    protected readonly IInput _input;
    protected readonly MoveXComponent _moveXComponent;
    public GroundState(IInput input, MoveXComponent moveXComponent, JumpComponent jumpComponent, Fsm fsm) : base(fsm)
    {
        _jumpComponent = jumpComponent;
        _input = input;
        _moveXComponent = moveXComponent;
    }

    public override void Enter()
    {
        _jumpComponent.IsGrounded.Subscribe(OnGrounded);
        _input.OnMoveEvent += OnTryMove;
        _input.OnJumpEvent += OnTryJump;
        OnTryMove(_input.MoveDirection);
    }

    private void OnTryJump()
    {
        if (_jumpComponent.CanJump.Value)
        {
            _fsm.SetState<JumpState>();
        }
    }

    private void OnTryMove(float direction)
    {
        if (direction == 0)
        {
            _fsm.SetState<IdleState>();
        }
        else
        {
            _fsm.SetState<MoveState>();
        }
    }

    public override void Exit()
    {
        _jumpComponent.IsGrounded.Unsubscribe(OnGrounded);
        _input.OnMoveEvent -= OnTryMove;
    }
    private void OnGrounded(bool isGrouned)
    {
        if (!isGrouned)
        {
            _fsm.SetState<FallState>();
        }
    }
}
