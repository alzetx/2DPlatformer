using Atomic.Elements;
using UnityEngine;

public class JumpAnimator
{
    private Animator _animator;
    private IAtomicObservable _jumpEvent;

    public JumpAnimator(Animator animator, IAtomicObservable onJump)
    {
        _animator = animator;
        _jumpEvent = onJump;
    }

    public void Enable()
    {
        _jumpEvent.Subscribe(OnJump);
    }

    public void Disable()
    {
        _jumpEvent.Unsubscribe(OnJump);
    }
    private void OnJump()
    {
        _animator.SetTrigger(GameConstants.AnimatorKeys.Jump);
    }
}
