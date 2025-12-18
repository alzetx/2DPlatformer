using Atomic.Elements;

public class JumpCountCondition : IAtomicFunction<bool>
{
    private readonly IAtomicValue<int> _max;
    private readonly IAtomicValue<int> _used;

    public JumpCountCondition(IAtomicValue<int> max, IAtomicValue<int> used)
    {
        _max = max;
        _used = used;
    }

    public bool Invoke()
    {
        return _used.Value < _max.Value;
    }
}
