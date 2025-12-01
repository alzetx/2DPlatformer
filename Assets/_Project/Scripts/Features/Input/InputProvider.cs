using System;
using UnityEngine.InputSystem;
using Zenject;

public class InputProvider : IInitializable, IGamePauseListener, IGameResumeListener, IDisposable, IInput
{
    private Controls _input;
    private float _moveDirection;
    public ref float MoveDirection => ref _moveDirection;

    public event Action<float> OnMoveEvent;
    public event Action OnJumpEvent;

    void IInitializable.Initialize()
    {
        _input = new();
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

    public void Dispose()
    {
        Bind(false);
        _input.Dispose();
    }

    private void Bind(bool enable)
    {
        if (enable)
        {
            _input.Character.Enable();
            _input.Character.Movement.performed += OnMove;
            _input.Character.Movement.canceled += OnMove;
            _input.Character.Jump.performed += OnJump;
        }
        else
        {
            _input.Character.Movement.performed -= OnMove;
            _input.Character.Movement.canceled -= OnMove;
            _input.Character.Jump.performed -= OnJump;
            _input.Character.Disable();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<float>();
        OnMoveEvent?.Invoke(_moveDirection);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }
    
}
