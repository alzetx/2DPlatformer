//public class MoveState : GroundState
//{
//    public MoveState(IInput input, MoveXComponent moveComponent, JumpComponent jumpComponent, Fsm fsm) : base(input, moveComponent, jumpComponent, fsm)
//    {
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        _input.OnMoveEvent += OnMove;
//        OnMove(_input.MoveDirection);
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        _input.OnMoveEvent -= OnMove;
//    }
//    private void OnMove(float direction)
//    {
//        _moveXComponent.xDirection.Value = direction;
//    }
//}