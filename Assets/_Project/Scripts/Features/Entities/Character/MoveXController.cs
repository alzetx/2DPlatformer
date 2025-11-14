using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using TMPro.Examples;

public class MoveXController
{
    private readonly AtomicObject _character;
    private IAtomicVariable<float> _xDirection;
    private readonly IInput _input;

    public MoveXController(AtomicObject character, IInput input)
    {
        _character = character;
        _input = input;
    }

    public void Initialize()
    {
        _xDirection = GetMoveDirection();
    }

    public void OnEnable()
    {
        _input.OnMoveEvent += OnMove;
    }

    public void OnDisable()
    {
        _input.OnMoveEvent -= OnMove;
    }

    private void OnMove(float direction)
    {
        _xDirection.Value = direction;
    }

    private IAtomicVariable<float> GetMoveDirection()
    {
        var direction = _character.GetVariable<float>(Names.Variable.XMoveDirection);
        return direction ?? default;
    }
}

public class JumpController
{
    private readonly AtomicObject _character;
    private IAtomicAction _jumpAction;
    private readonly IInput _input;

    public JumpController(AtomicObject character, IInput input)
    {
        _character = character;
        _input = input;
    }

    public void Initialize()
    {
        _jumpAction = GetJumpAction();
    }

    public void OnEnable()
    {
        _input.OnJumpEvent += OnJump;
    }

    public void OnDisable()
    {
        _input.OnJumpEvent -= OnJump;
    }

    private void OnJump()
    {
        _jumpAction.Invoke();
    }

    private IAtomicAction GetJumpAction()
    {
        var jumpAction = _character.GetAction(Names.Actions.Jump);
        if (jumpAction != null)
        {
            return jumpAction;
        }
        return default;
    }
}

