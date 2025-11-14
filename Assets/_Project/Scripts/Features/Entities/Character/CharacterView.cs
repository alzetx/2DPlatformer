using System;
using UnityEngine;

[Serializable]
public class CharacterView
{
    [SerializeField]
    private Animator _animator;

    private MoveAnimator _moveAnimator;
    private AirAnimator _airAnimator;
    private JumpAnimator _jumpAnimator;
    public void OnStartGame(CharacterCore core)
    {
        _moveAnimator = new(_animator, core.MoveComponent.IsMoving);
        _airAnimator = new(_animator, core.JumpComponent.IsGroundedObservable, core.JumpComponent.IsGrounded.Value, core.JumpComponent.Rigidbody);
        _jumpAnimator = new(_animator, core.JumpComponent.onJump);
    }
    public void OnEnable()
    {
        Bind(true);
    }

    public void Tick(float deltaTime)
    {
        _airAnimator.Update();
    }
    public void OnDisable()
    {
        Bind(false);
    }

    private void Bind(bool bind)
    {
        if (bind)
        {
            _moveAnimator.Enable();
            _airAnimator.Enable();
            _jumpAnimator.Enable();
        }
        else
        {
            _moveAnimator.Disable();
            _airAnimator.Disable();
            _jumpAnimator.Disable();
        }
    }
}
