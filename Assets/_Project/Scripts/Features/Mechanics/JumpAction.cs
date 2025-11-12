using Atomic.Elements;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class JumpAction : IAtomicAction
{
    private IAtomicValue<bool> _condition;
    private IAtomicAction _addForceJumpAction;
    private IAtomicEvent _onJump;

    public JumpAction(IAtomicValue<bool> condition, IAtomicAction addForceJumpAction, IAtomicEvent onJump)
    {
        _condition = condition;
        _addForceJumpAction = addForceJumpAction;
        _onJump = onJump;
    }

    [Button]
    public void Invoke()
    {
        if (!_condition.Value)
        {
            return;
        }
        _addForceJumpAction.Invoke();
        _onJump.Invoke();
    }
}
