using Atomic.Elements;
using UnityEngine;

public sealed class OnTriggerCollisionMechanics
{
    private readonly IAtomicAction<bool, Collider2D>[] _actions;

    public OnTriggerCollisionMechanics(params IAtomicAction<bool, Collider2D>[] actions)
    {
        _actions = actions;
    }

    public void Enter(Collider2D c)
    {
        foreach (var a in _actions)
            a.Invoke(true, c);
    }

    public void Exit(Collider2D c)
    {
        foreach (var a in _actions)
            a.Invoke(false, c);
    }
}
