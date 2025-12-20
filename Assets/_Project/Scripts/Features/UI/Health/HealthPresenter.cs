public class HealthPresenter
{
    private readonly SliderCurrencyView _slider;
    private readonly HPView _text;
    private readonly HealthData _health;

    public HealthPresenter(SliderCurrencyView slider, HPView text, HealthData health)
    {
        _slider = slider;
        _text = text;
        _health = health;
    }
    public void Initialize()
    {
        _slider.SetMaxValue(_health.Max.Value);
        _slider.SetCurrentValue(_health.Min.Value);
        _text.SetupCurrency(_health.Current.Value.ToString());

        _slider.ChangeValue(_health.Current.Value);
        _text.AddCurrency(0, _health.Current.Value);
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
            _health.OnValueChangedEvent +=OnHealthChanged;
        }
        else
        {
            _health.OnValueChangedEvent -=OnHealthChanged;
        }
    }

    private void OnHealthChanged(int previous, int current)
    {
        if (previous == current)
        {
            return;
        }

        _slider.ChangeValue(current);
        if (previous < current)
        {
            _text.AddCurrency(previous, current- previous);
        }
        else
        {
            _text.RemoveCurrency(previous.ToString());
        }

    }
}
