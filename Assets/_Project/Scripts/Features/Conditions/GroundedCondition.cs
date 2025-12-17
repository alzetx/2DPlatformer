using Atomic.Elements;
using System.Collections.Generic;

public class GroundedCondition<T> : IAtomicFunction<bool>
{
    private IAtomicValue<bool> _enableBehaviour;
    private IAtomicObservable<IReadOnlyList<T>> _obstacles;
    private bool _enable;

    public GroundedCondition(IAtomicValue<bool> behaviour, IAtomicObservable<IReadOnlyList<T>> obstacles)
    {
        _enableBehaviour = behaviour;
        _obstacles = obstacles;
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
        _enable = list.Count > 0;
    }

    public bool Invoke()
    {
        return _enableBehaviour.Value && _enable;
    }
}
