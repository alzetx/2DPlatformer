using Atomic.Elements;
using System;

[Serializable]
public abstract class Storage<T> : IDisposable
{
    public abstract IAtomicValue<T> Current { get; }
    public delegate void ValueChangedHandler<T>(T previous, T current);

    public abstract void AddValue(T value);
    public abstract void SpendValue(T value);

    protected abstract void SetValue(T newValue);

    public virtual void Dispose() { }
}
