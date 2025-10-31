using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class GameKernel :
    MonoKernel, 
    IGameStartListener,
    IGameResumeListener,
    IGamePauseListener,
    IGameFinishListener
{
    [Inject]
    private GameStateMachine _gameState;

    [InjectLocal]
    private List<IGameListener> _listeners = new();

    [Inject(Optional = true, Source = InjectSources.Local)]
    private List<IGameTickable> _tickables = new();

    [Inject(Optional = true, Source = InjectSources.Local)]
    private List<IGameFixedTickable> _fixedTickables = new();

    [Inject(Optional = true, Source = InjectSources.Local)]
    private List<IGameLateTickable> _lateTickables = new();
    public override  void Start()
    {
        base.Start();
        _gameState.AddListener(this);
    }
    public override void Update()
    {
        base.Update();

        if (_gameState.State != GameState.Play)
        {
            return;
        }

        float deltaTime = Time.deltaTime;
        foreach (var tickable in _tickables)
        {
            tickable.Tick(deltaTime);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_gameState.State != GameState.Play)
        {
            return;
        }

        float deltaTime = Time.fixedDeltaTime;
        foreach (var tickable in _fixedTickables)
        {
            tickable.FixedTick(deltaTime);
        }
    }
    public override void LateUpdate()
    {
        base.LateUpdate();

        if (_gameState.State != GameState.Play)
        {
            return;
        }

        float deltaTime = Time.deltaTime;
        foreach (var tickable in _lateTickables)
        {
            tickable.LateTick(deltaTime);
        }
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        _gameState.RemoveListener(this);
    }
    void IGameStartListener.OnStartGame()
    {
        foreach (var it in _listeners)
        {
            if (it is IGameStartListener listener)
            {
                listener.OnStartGame();
            }
        }
    }

    void IGamePauseListener.OnPauseGame()
    {
        foreach (var it in _listeners)
        {
            if (it is IGamePauseListener listener)
            {
                listener.OnPauseGame();
            }
        }
    }

    void IGameResumeListener.OnResumeGame()
    {
        foreach (var it in _listeners)
        {
            if (it is IGameResumeListener listener)
            {
                listener.OnResumeGame();
            }
        }
    }

    void IGameFinishListener.OnFinishGame()
    {
        foreach (var it in _listeners)
        {
            if (it is IGameFinishListener listener)
            {
                listener.OnFinishGame();
            }
        }
    }
}
