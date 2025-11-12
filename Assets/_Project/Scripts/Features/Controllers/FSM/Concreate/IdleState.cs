public class IdleState : GroundState
{
    public IdleState(IInput input, MoveXComponent moveXComponent, JumpComponent jumpComponent, Fsm fsm) : base(input, moveXComponent, jumpComponent, fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _moveXComponent.xDirection.Value = 0f;
    }
}
