using System;
using UnityEngine;

[Serializable]
public class CharacterView
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private HealthView _healthView;

    private MoveAnimator _moveAnimator;
    private AirAnimator _airAnimator;
    private JumpAnimator _jumpAnimator;

    private HealthPresenter _healthPresenter;
    public void OnStartGame(CharacterCore core)
    {
        _moveAnimator = new(_animator, core.moveComponent.IsMoving);
        _airAnimator = new(_animator, core.jumpComponent.IsGroundedObservable, core.jumpComponent.IsGrounded.Value, core.jumpComponent.Rigidbody);
        _jumpAnimator = new(_animator, core.jumpComponent.onJump);
        _healthPresenter = new(_healthView, core.healthComponent.HealthData);
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
            _healthPresenter.Enable();
        }
        else
        {
            _moveAnimator.Disable();
            _airAnimator.Disable();
            _jumpAnimator.Disable();
            _healthPresenter.Disable();
        }
    }
}
