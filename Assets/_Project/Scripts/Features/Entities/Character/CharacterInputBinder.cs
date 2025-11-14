using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

public class CharacterInputBinder :  IGameStartListener, IGamePauseListener, IGameResumeListener
{
    private readonly AtomicObject _character;
    private readonly IInput _input;

    private MoveXController _moveXController;

    public CharacterInputBinder(AtomicObject character, IInput input)
    {
        _character = character;
        _input = input;
    }

    void IGameStartListener.OnStartGame()
    {
        LinksInput();
        Bind(true);
    }

   

    void IGamePauseListener.OnPauseGame()
    {
        Bind(false);
    }

    void IGameResumeListener.OnResumeGame()
    {
        Bind(true);
    }
    private void LinksInput()
    {
        _moveXController = new(_character, _input);
        _moveXController.Initialize();
    }
    private void Bind(bool bind)
    {
        if (bind)
        {
            _moveXController.OnEnable();
        }
        else
        {
            _moveXController.OnDisable();
        }
    }

    private class MoveXController
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
}


