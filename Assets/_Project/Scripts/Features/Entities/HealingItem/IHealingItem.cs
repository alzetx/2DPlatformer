using Atomic.Elements;
using Atomic.Objects;

[Is("Pickable", "Healing")] //todo вынести 
public interface IHealingItem : IAtomicObject
{
    IAtomicValue<int> HealingPoints { get; }

    IAtomicAction PickUpAction { get; }
}