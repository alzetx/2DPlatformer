using Atomic.Elements;

public class JumpPermissionCondition : IAtomicFunction<bool>
{
    private readonly IAtomicValue<bool> _behaviour;
    private readonly IAtomicValue<bool> _grounded;
    private readonly IAtomicValue<bool> _hasExtraJumps;

    public JumpPermissionCondition(
        IAtomicValue<bool> behaviour,
        IAtomicValue<bool> grounded,
        IAtomicValue<bool> hasExtraJumps)
    {
        _behaviour = behaviour;
        _grounded = grounded;
        _hasExtraJumps = hasExtraJumps;
    }

    public bool Value =>
        _behaviour.Value &&
        (_grounded.Value || _hasExtraJumps.Value);

    public bool Invoke()
    {
        return _behaviour.Value && (_grounded.Value || _hasExtraJumps.Value);
    }
}
