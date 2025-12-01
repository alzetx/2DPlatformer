using System;
using UnityEngine;

[Serializable]
public class CharacterView
{
    [SerializeField]
    private Animator _animator;
    private HealthView _healthView;

    private MoveAnimator _moveAnimator;
    private AirAnimator _airAnimator;
    private JumpAnimator _jumpAnimator;
    private DeathAnimator _deathAnimator;

    private HealthPresenter _healthPresenter;
    public void Construct(HealthView healthView)
    {
        _healthView = healthView;
    }
    public void OnStartGame(CharacterCore core)
    {
        _deathAnimator = new(_animator, core.healthComponent.HealthData);
        _moveAnimator = new(_animator, core.moveComponent.IsMoving);
        _airAnimator = new(_animator, core.jumpComponent.IsGroundedObservable, core.jumpComponent.IsGrounded.Value, core.jumpComponent.Rigidbody);
        _jumpAnimator = new(_animator, core.jumpComponent.onJump);
        _healthPresenter = new(_healthView, core.healthComponent.HealthData);
        _healthPresenter.Initialize();
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
            _deathAnimator.Enable();
            _moveAnimator.Enable();
            _airAnimator.Enable();
            _jumpAnimator.Enable();
            _healthPresenter.Enable();
        }
        else
        {
            _deathAnimator.Disable();
            _moveAnimator.Disable();
            _airAnimator.Disable();
            _jumpAnimator.Disable();
            _healthPresenter.Disable();
        }
    }
}
