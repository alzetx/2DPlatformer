using Atomic.Elements;
using UnityEngine;

public sealed class ContactSetUpdateAction : IAtomicAction<bool, Collider2D>
{
    private readonly IColliderFilter _filter;
    private readonly AtomicCollection<GameObject> _collection;

    public ContactSetUpdateAction(IColliderFilter filter, AtomicCollection<GameObject> collection)
    {
        _filter = filter;
        _collection = collection;
    }

    public void Invoke(bool enter, Collider2D c)
    {
        if (!_filter.Match(c))
            return;

        if (enter)
            _collection.Add(c.gameObject);
        else
            _collection.Remove(c.gameObject);
    }
}
