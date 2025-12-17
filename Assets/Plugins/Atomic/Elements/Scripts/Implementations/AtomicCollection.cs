using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicCollection<T> : IAtomicObservable<IReadOnlyList<T>>, IDisposable, IEnumerable<T>
    {
        [SerializeField]
        private List<T> _list = new List<T>();
        private Action<IReadOnlyList<T>> _onChanged;


        public AtomicCollection()
        {
            _list = new List<T>();
        }

        public AtomicCollection(ICollection<T> list)
        {
            _list = list.ToList();
        }
        public void Subscribe(Action<IReadOnlyList<T>> listener) => _onChanged += listener;
        public void Unsubscribe(Action<IReadOnlyList<T>> listener) => _onChanged -= listener;

        private void Notify() => _onChanged?.Invoke(_list.AsReadOnly());

        public void Add(T item)
        {
            _list.Add(item);
            Notify();
        }

        public bool Remove(T item)
        {
            var removed = _list.Remove(item);
            if (removed) Notify();
            return removed;
        }

        public void Clear()
        {
            _list.Clear();
            Notify();
        }

        public T this[int index]
        {
            get => _list[index];
            set
            {
                _list[index] = value;
                Notify();
            }
        }

        public int Count => _list.Count;

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public void Dispose() => _onChanged = null;
    }
}
