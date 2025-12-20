using Atomic.Elements;
using UnityEngine;

public class TryPickUpCoinAction : IAtomicAction<bool, Collider2D>
{
    private readonly IAtomicAction<IPickUpItem<int>> _pickUpCoinAction;
    public TryPickUpCoinAction(IAtomicAction<IPickUpItem<int>> pickUpCoinAction)
    {
        _pickUpCoinAction = pickUpCoinAction;
    }
    public void Invoke(bool onEnter, Collider2D collider)
    {
        IPickUpItem<int> coin = collider.GetComponent<IPickUpItem<int>>();
        if (onEnter && coin != null && coin.Is(GameConstants.ObjectTypes.Coin))
        {
            _pickUpCoinAction.Invoke(coin);
            coin.PickUpAction.Invoke();
        }
    }
}
