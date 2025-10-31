using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    public event Action OnGameStarted;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameFinished;

    public GameState State { get; private set; }

    private List<IGameListener> _listeners = new();

    public void AddListener(IGameListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(IGameListener listener)
    {
        _listeners.Remove(listener);
    }

    [Button]
    public void StartGame()
    {
        if (State != GameState.Off)
        {
            return;
        }

        foreach (var it in _listeners)
        {
            if (it is IGameStartListener listener)
            {
                listener.OnStartGame();
            }
        }

        State = GameState.Play;
        OnGameStarted?.Invoke();
        Debug.Log("Game Started");
    }

    [Button]
    public void PauseGame()
    {
        if (State != GameState.Play)
        {
            return;
        }

        foreach (var it in _listeners)
        {
            if (it is IGamePauseListener listener)
            {
                listener.OnPauseGame();
            }
        }

        State = GameState.Pause;
        OnGamePaused?.Invoke();
        Debug.Log("Game Paused");
    }

    [Button]
    public void ResumeGame()
    {
        if (State != GameState.Pause)
        {
            return;
        }

        foreach (var it in _listeners)
        {
            if (it is IGameResumeListener listener)
            {
                listener.OnResumeGame();
            }
        }

        State = GameState.Play;
        OnGameResumed?.Invoke();
        Debug.Log("Game Resumed");
    }

    [Button]
    public void FinishGame()
    {
        if (State is not  (GameState.Pause or GameState.Play))
        {
            return;
        }

        foreach (var it in _listeners)
        {
            if (it is IGameFinishListener listener)
            {
                listener.OnFinishGame();
            }
        }

        State = GameState.Off;
        OnGameFinished?.Invoke();
        Debug.Log("Game Finished");
    }
}

public enum GameState
{
    Off,
    Play,
    Pause
}