using Atomic.Objects;
using UnityEngine;
using Zenject;

public sealed class Character : AtomicObject, IInitializable, IGameFixedTickable, IGamePauseListener, IGameResumeListener, IGameTickable
{
    [SerializeField, Section]
    private CharacterCore _core;
    [SerializeField, Section]
    private CharacterView _view;

    [Inject]
    private void Construct(SliderCurrencyView sliderHP, HPView textHP, CoinView coinView)
    {
        _view.Construct(sliderHP, textHP, coinView);
    }
    void IInitializable.Initialize()
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
    private void OnDestroy()
    {
        _core.OnDestroy();
    }
}
