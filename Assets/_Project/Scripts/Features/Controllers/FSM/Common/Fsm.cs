using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[SerializeField]
public class Fsm
{
    protected Dictionary<Type, FsmState> _states = new();

    protected FsmState _currentState;

    [ShowInInspector, ReadOnly, HideInEditorMode]
    public string CurrentState => _currentState?.GetType().Name ?? "None";

    public void AddStates(params FsmState[] states)
    {
        foreach (var state in states)
        {
            AddState(state);
        }
    }

    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState(Type stateType)
    {
        if (_currentState?.GetType() == stateType)
        {
            return;
        }

        if (_states.TryGetValue(stateType, out var newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        else
        {
            Debug.LogError($"FSM has no state of type {stateType}");
        }
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if (_currentState?.GetType() == type)
        {
            return;
        }
        if (_states.TryGetValue(type, out var newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            Debug.Log($"Set switch to {CurrentState}"); //todo Test
            _currentState.Enter();
        }
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
