using Atomic.Elements;
using UnityEngine;

public class AirAnimator
{
    private Animator _animator;
    private IAtomicObservable<bool> _onGrounded;
    private bool _onGround;
    private Rigidbody2D _rigidbody;

    public AirAnimator(Animator animator, IAtomicObservable<bool> onGrounded, bool onGround,Rigidbody2D rigidbody)
    {
        _animator = animator;
        _onGrounded = onGrounded;
        _onGround = onGround;
        _rigidbody = rigidbody;
    }

    public void Enable()
    {
        _onGrounded.Subscribe(OnGround);
    }

    public void Update()
    {
        if (_onGround)
            return;
        _animator.SetFloat(GameConstants.AnimatorKeys.YVelocity, _rigidbody.linearVelocityY);
    }

    private void OnGround(bool onGround)
    {
        _onGround = onGround;
        _animator.SetBool(GameConstants.AnimatorKeys.IsGrounded, _onGround);
    }

    public void Disable()
    {
        _onGrounded.Unsubscribe(OnGround);
    }

}
