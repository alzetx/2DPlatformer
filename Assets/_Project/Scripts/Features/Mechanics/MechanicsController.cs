using Atomic.Elements;

public class MechanicsController
{
    private readonly IAtomicObservable<bool> _isAlive;
    private readonly IAtomicVariable<bool>[] _mechanicsBehaviour;

    public MechanicsController(IAtomicObservable<bool> isAlive,params IAtomicVariable<bool>[] mechanicsBehaviour)
    {
        _isAlive = isAlive;
        _mechanicsBehaviour = mechanicsBehaviour;
    }

    public void Enable()
    {
        _isAlive.Subscribe(OnValueChanged);
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
