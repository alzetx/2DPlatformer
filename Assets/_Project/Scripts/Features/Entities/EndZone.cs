using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Atomic.Extensions;

public class EndZone : AtomicObject
{
    [SerializeField]
    private LayerMask _interactionMasks;
    [SerializeField]
    private TriggerColliderDispatcher _killZoneCollider;

    private AtomicAction<bool, Collider2D> _onColliderEnter;
    private AtomicAction<Collider2D> _killAction;
    private LayerMaskFilter _layerFilter;
    private void Awake()
    {
        Compose();
        _layerFilter = new(_interactionMasks);
        _onColliderEnter = new(OnColliderEnter);
        _killZoneCollider.Initialize(_onColliderEnter);
        _killAction = new(Kill);
    }

    private void Kill(Collider2D target)
    {
        if (target.TryGetComponent<IAtomicObject>(out var atomicObject) &&
            atomicObject.Is(GameConstants.ObjectTypes.Damageable))
        {
            var KillAction = atomicObject.GetAction(GameConstants.Actions.Kill);
            KillAction?.Invoke();
        }
    }

    private void OnColliderEnter(bool enter, Collider2D collider)
    {
        if (enter && _layerFilter.Match(collider))
        { 
            _killAction.Invoke(collider);
        }
    }

}
