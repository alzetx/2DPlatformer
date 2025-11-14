using Atomic.Objects;
using UnityEngine;

public class Character : AtomicObject, IGameStartListener, IGameFixedTickable, IGamePauseListener, IGameResumeListener
{
    [SerializeField]
    [Section]
    private CharacterCore _core;
    [SerializeField]
    [Section]
    private CharacterView _view;
    public void OnStartGame()
    {
        Compose();
        _core.OnStartGame();
        _view.OnStartGame(_core);
        Enable();
    }

    public void FixedTick(float deltaTime)
    {
        _core.FixedTick(deltaTime);
    }
    private void OnDestroy()
    {
        _core.OnDestroy();
    }

    public void OnPauseGame()
    {
        Disable();
    }

    public void OnResumeGame()
    {
        Enable();
    }

    private void Enable()
    {
        _core.OnEnable();
        _view.OnEnable();
    }

    private void Disable()
    {
        _core.OnDisable();
        _view.OnDisable();
    }
}
