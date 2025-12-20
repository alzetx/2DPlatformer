using Atomic.Elements;

public class AddCoinAction : IAtomicAction<IPickUpItem<int>>
{
    private readonly CoinStorage _coinSystem;

    public AddCoinAction(CoinStorage coinSystem)
    {
        _coinSystem = coinSystem;
    }

    public void Invoke(IPickUpItem<int> coin)
    {
        _coinSystem.AddValue(coin.Points.Value);
    }
}
