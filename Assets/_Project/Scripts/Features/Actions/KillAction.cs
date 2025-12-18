using Atomic.Elements;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class KillAction : IAtomicAction
{
    private IAtomicValue<bool> _condition;
    private IAtomicAction _killAction;
    private IAtomicEvent _onKill;

    public void Initialize(IAtomicValue<bool> condition, IAtomicAction killAction, IAtomicEvent onKill)
    {
        _condition = condition;
        _killAction = killAction;
        _onKill = onKill;
    }

    [Button]
    public void Invoke()
    {
        if (!_condition.Value)
        {
            return;
        }
        _killAction.Invoke();
        _onKill.Invoke();
    }
}
