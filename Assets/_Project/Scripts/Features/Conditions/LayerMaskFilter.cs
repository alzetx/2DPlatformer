using UnityEngine;

public sealed class LayerMaskFilter : IColliderFilter
{
    private readonly LayerMask[] _masks;

    public LayerMaskFilter(params LayerMask[] masks)
    {
        _masks = masks;
    }

    public bool Match(Collider2D collider)
    {
        int layerBit = 1 << collider.gameObject.layer;
        foreach (var mask in _masks)
        {
            if ((mask.value & layerBit) != 0)
                return true;
        }
        return false;
    }
}

public interface IColliderFilter
{
    bool Match(Collider2D collider);
}
