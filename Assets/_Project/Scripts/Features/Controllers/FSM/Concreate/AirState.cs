using UnityEngine;

public class AirState : FsmState
{
    protected readonly JumpComponent _jumpComponent;
    protected readonly Rigidbody2D _rigidbody;

    public AirState(JumpComponent jumpComponent, Rigidbody2D rigidbody2D, Fsm fsm) : base(fsm)
    {
        _jumpComponent = jumpComponent;
        _rigidbody = rigidbody2D;
    }

    public override void Enter()
    {
        _jumpComponent.IsGrounded.Subscribe(OnGrounded);
    }
    public override void Exit()
    {
        _jumpComponent.IsGrounded.Unsubscribe(OnGrounded);
    }
    private void OnGrounded(bool isGrouned)
    {
        if (isGrouned)
        { 
            _fsm.SetState<MoveState>();
        }
    }
}
