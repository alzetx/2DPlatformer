using Atomic.Elements;
using Atomic.Extensions;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class JumpAction : IAtomicAction
{
    private IAtomicValue<bool>[] _conditions;
    private IAtomicAction _addForceJumpAction;
    private IAtomicEvent _onJump;

    public void Initialize(IAtomicAction addForceJumpAction, IAtomicEvent onJump,  params IAtomicValue<bool>[] conditions)
    {
        _conditions = conditions;
        _addForceJumpAction = addForceJumpAction;
        _onJump = onJump;
    }

    [Button]
    public void Invoke()
    {
        for (int i = 0; i < _conditions.Length; i++)
        {
            if (!_conditions[i].Value)
            {
                return;
            }
        }


        _addForceJumpAction.Invoke();
        _onJump.Invoke();
    }
}
