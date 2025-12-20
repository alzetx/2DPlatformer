using Atomic.Elements;
using Unity.Mathematics;

public class TakeDamageMechanics
{
    private readonly IAtomicObservable<int> _takeDamageEvent;
    private readonly HealthData _hitPoints;

    public TakeDamageMechanics(IAtomicObservable<int> takeDamageEvent, HealthData hitPoints)
    {
        _takeDamageEvent = takeDamageEvent;
        _hitPoints = hitPoints;
    }

    public void OnEnable()
    {
        _takeDamageEvent.Subscribe(OnTakeDamage);
    }

    public void OnDisable()
    {
        _takeDamageEvent.Unsubscribe(OnTakeDamage);
    }

    private void OnTakeDamage(int damage)
    {
        damage = math.clamp(damage, 0, int.MaxValue);
        _hitPoints.SpendValue(damage);
    }
}