using Atomic.Elements;
using System.Collections.Generic;

public class GroundedCondition<T> : IAtomicFunction<bool>
{
    private readonly IAtomicObservable<IReadOnlyList<T>> _obstacles;
    private readonly IAtomicVariable<bool> _isGrounded;

    public GroundedCondition(IAtomicObservable<IReadOnlyList<T>> obstacles, IAtomicVariable<bool> isGrouned)
    {
        _obstacles = obstacles;
        _isGrounded = isGrouned;
    }

    public void Enable()
    {
        _obstacles.Subscribe(OnObstaclesChanged);
    }

    public void Disable()
    {
        _obstacles.Unsubscribe(OnObstaclesChanged);
    }

    private void OnObstaclesChanged(IReadOnlyList<T> list)
    {
        _isGrounded.Value = list.Count > 0;
    }

    public bool Invoke()
    {
        return  _isGrounded.Value;
    }
}
