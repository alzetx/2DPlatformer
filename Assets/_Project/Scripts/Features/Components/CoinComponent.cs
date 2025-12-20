using System;
using UnityEngine;

[Serializable]
public class CoinComponent
{
    [SerializeField]
    private CoinStorage _coinSystem;

    [SerializeField]
    private TriggerColliderDispatcher _colliderDispatcher;
    [SerializeField]
    private TryPickUpCoinAction _tryPickUpAction;
    [SerializeField]
    private AddCoinAction _addCoinAction;

    public CoinStorage CoinStorage => _coinSystem;

    public void Initialize()
    {
        _addCoinAction = new(_coinSystem);
        _tryPickUpAction = new(_addCoinAction);
        _colliderDispatcher.Initialize(_tryPickUpAction);
    }
}
