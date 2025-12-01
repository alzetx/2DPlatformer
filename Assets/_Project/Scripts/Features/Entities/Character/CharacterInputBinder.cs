using Atomic.Objects;
using Zenject;

public class CharacterInputBinder :  IInitializable, IGamePauseListener, IGameResumeListener
{
    private readonly AtomicObject _character;
    private readonly IInput _input;

    private MoveXController _moveXController;
    private JumpController _jumpController;

    public CharacterInputBinder(AtomicObject character, IInput input)
    {
        _character = character;
        _input = input;
    }

    void IInitializable.Initialize()
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

        _jumpController = new(_character, _input);
        _jumpController.Initialize();
    }
    private void Bind(bool bind)
    {
        if (bind)
        {
            _moveXController.OnEnable();
            _jumpController.OnEnable();
        }
        else
        {
            _moveXController.OnDisable();
            _jumpController.OnDisable();
        }
    }
}


