using Atomic.Elements;

public class MechanicsController
{
    private readonly IAtomicObservable<bool> _isAlive;
    private readonly IAtomicValue<bool> _isAliveValue;
    private readonly IAtomicVariable<bool>[] _mechanicsBehaviour;

    public MechanicsController(IAtomicValue<bool> isAliveValue, IAtomicObservable<bool> isAlive,params IAtomicVariable<bool>[] mechanicsBehaviour)
    {
        _isAliveValue = isAliveValue;
        _isAlive = isAlive;
        _mechanicsBehaviour = mechanicsBehaviour;
    }

    public void Enable()
    {
        _isAlive.Subscribe(OnValueChanged);
        OnValueChanged(_isAliveValue.Value);
    }
    public void Disable()
    {
        _isAlive.Subscribe(OnValueChanged);
    }
    private void OnValueChanged(bool isAlive)
    {
        foreach (var behaviour in _mechanicsBehaviour)
        {
            behaviour.Value = isAlive;
        }
    }
}
