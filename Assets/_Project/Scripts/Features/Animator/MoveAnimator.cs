using Atomic.Elements;
using UnityEngine;

public class MoveAnimator
{
    private readonly Animator _animator;
    private readonly IAtomicObservable<bool> _isMove;

    public MoveAnimator(Animator animator, IAtomicObservable<bool> isMove)
    {
        _animator = animator;
        _isMove = isMove;
    }

    public void Enable()
    {
        _isMove.Subscribe(OnMove);
    }

    public void Disable()
    {
        _isMove.Unsubscribe(OnMove);
    }

    private void OnMove(bool onMove)
    {
        _animator.SetBool(Names.Animator.Moving, onMove);
    }
}
