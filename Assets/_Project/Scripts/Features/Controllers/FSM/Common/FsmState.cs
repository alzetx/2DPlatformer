public abstract class FsmState
{
    protected readonly Fsm _fsm;

    public FsmState(Fsm fsm)
    {
        _fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
