public class CoinPresenter
{
    private readonly CoinView _view;
    private readonly CoinStorage _coin;

    public CoinPresenter(CoinView view, CoinStorage coinStorage)
    {
        _view = view;
        _coin = coinStorage;
    }

    public void Initialize()
    {
        _view.SetupCurrency(_coin.Current.Value.ToString());
        _view.AddCurrency(0, _coin.Current.Value);
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
            _coin.OnValueChangedEvent += OnHealthChanged;
        }
        else
        {
            _coin.OnValueChangedEvent -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(int previous, int current)
    {
        if (previous == current)
        {
            return;
        }

        if (previous < current)
        {
            _view.AddCurrency(previous, current - previous);
        }
        else
        {
            _view.RemoveCurrency(previous.ToString());
        }

    }
}
