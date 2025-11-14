using Atomic.Elements;

public class DeathMechanics
{
    private readonly HealthData _health;
    private readonly IAtomicAction _deathEvent;

    public DeathMechanics(HealthData health, IAtomicEvent deathEvent)
    {
        _health = health;
        _deathEvent = deathEvent;
    }

    public void OnEnable()
    {
        _health.OnHealthChanged += OnHealthChanged;
    }

    public void OnDisable()
    {
        _health.OnHealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(int currentHp)
    {
        if (currentHp <= 0)
        {
            _deathEvent.Invoke();
        }
    }
}
