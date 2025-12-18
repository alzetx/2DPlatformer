public class HealthPresenter
{
    private readonly HealthView _view;
    private readonly HealthData _health;

    public HealthPresenter(HealthView view, HealthData health)
    {
        _view = view;
        _health = health;
    }
    public void Initialize()
    {
        _view.SetMaxValue(_health.MaxHP.Value);
        _view.SetCurrentValue(_health.CurrentHP.Value);
    }
    public void Enable()
    {
        Bind(true);
    }

    public void Disable()
    {
        Bind(false);
    }

    private void Bind(bool bind)
    {
        if (bind)
        {
            _health.OnHealthChanged.Subscribe(OnHealthChanged);
        }
        else
        {
            _health.OnHealthChanged.Unsubscribe(OnHealthChanged);
        }
    }

    private void OnHealthChanged(int currentHP)
    {
        _view.SetCurrentValue(currentHP);
    }
}
