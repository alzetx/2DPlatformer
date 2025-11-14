using System;

public interface IInput
{
    ref float MoveDirection { get; }

    event Action<float> OnMoveEvent;
    event Action OnJumpEvent;
}
