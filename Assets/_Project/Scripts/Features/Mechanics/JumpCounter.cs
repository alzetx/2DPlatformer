using Atomic.Elements;

public class JumpCounter
{
    private readonly IAtomicObservable _jumpEvent;
    private readonly IAtomicObservable<bool> _onGroundEvent;
    private readonly IAtomicVariable<int> _usedCountJump;

    public JumpCounter(IAtomicObservable jumpEvent, IAtomicObservable<bool> onGround, IAtomicVariable<int> usedCountJump)
    {
        _jumpEvent = jumpEvent;
        _onGroundEvent = onGround;
        _usedCountJump = usedCountJump;
    }

    public void Enable()
    {
        _jumpEvent.Subscribe(OnJump);
        _onGroundEvent.Subscribe(OnGrounded);
    }
    public void Disable()
    {
        _jumpEvent.Unsubscribe(OnJump);
        _onGroundEvent.Unsubscribe(OnGrounded);
    }
    private void OnGrounded(bool onGround)
    {
        if (onGround)
        {
            Reset();
        }
    }
    
    public void Reset()
    {
        _usedCountJump.Value = 0;
    }
    private void OnJump()
    {
        _usedCountJump.Value++;
    }
}
