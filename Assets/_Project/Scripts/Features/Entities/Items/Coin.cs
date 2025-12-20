using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

[Is(GameConstants.ObjectTypes.Coin)]
public class Coin : AtomicObject, IPickUpItem<int>
{
    public IAtomicAction PickUpAction => _pickUpAction;
    public IAtomicValue<int> Points => _coins;

    [SerializeField]
    private AtomicAction _pickUpAction;
    [SerializeField]
    private AtomicValue<int> _coins;
    [SerializeField]
    private Collider2D _collider;

    [Header("View")]
    [SerializeField]
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _collider.isTrigger = true;
        Compose();
    }

    public override void Compose()
    {
        base.Compose();
        _pickUpAction.Compose(() =>
        {
            _sprite.enabled = false;
            _collider.enabled = false;
            Destroy(gameObject);
        });
    }
}

[Is(GameConstants.ObjectTypes.PickUp)]
public interface IPickUpItem<T> : IAtomicObject
{
    IAtomicValue<T> Points { get; }

    IAtomicAction PickUpAction { get; }
}

