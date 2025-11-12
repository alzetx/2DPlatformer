using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerColliderDispatcher : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider;

    public event Action<Collider2D> TriggerEnteredEvent;
    public event Action<Collider2D> TriggerExitedEvent;

    private void Start()
    {
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnteredEvent?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExitedEvent?.Invoke(collision);
    }
}