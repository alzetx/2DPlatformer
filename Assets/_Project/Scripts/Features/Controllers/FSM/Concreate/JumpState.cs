//using System;
//using UnityEngine;

//public class JumpState : AirState
//{
//    public JumpState(JumpComponent jumpComponent, Rigidbody2D rigidbody2D, Fsm fsm) : base(jumpComponent, rigidbody2D, fsm)
//    {
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        Jump();
//    }
//    public override void Update()
//    {
//        base.Update();
//        if (_rigidbody.linearVelocityY < 0)
//        {
//            _fsm.SetState<FallState>();
//        }
//    }
//    private void Jump()
//    {
//        _jumpComponent.Jump.Invoke();
//    }
//}
