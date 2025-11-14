using Atomic.Objects;
using UnityEngine;

public class Character : AtomicObject, IGameStartListener, IGameFixedTickable, IGamePauseListener, IGameResumeListener, IGameTickable
{
    [SerializeField]
    [Section]
    private CharacterCore _core;
    [SerializeField]
    [Section]
    private CharacterView _view;
    public void OnStartGame()
    {
        _core.OnStartGame();
        _view.OnStartGame(_core);
        Enable();
    }

    void IGameFixedTickable.FixedTick(float deltaTime)
    {
        _core.FixedTick(deltaTime);
    }
    void IGameTickable.Tick(float deltaTime)
    {
        _view.Tick(deltaTime);
    }

    private void OnDestroy()
    {
        _core.OnDestroy();
    }

    void IGamePauseListener.OnPauseGame()
    {
        Disable();
    }

    void IGameResumeListener.OnResumeGame()
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
