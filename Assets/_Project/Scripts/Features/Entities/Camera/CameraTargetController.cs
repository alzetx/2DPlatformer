using Atomic.Objects;
using System;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraTargetController : MonoBehaviour, IGameStartListener
{
    [SerializeField]
    private CinemachineCamera _cinemaCamera;

    [Inject]
    private PlayerSpawner _spawner;
    public void OnStartGame()
    {
        Bind(true);
    }
    private void OnDisable()
    {
        Bind(false);
    }
    private void OnPlayerSpawn(AtomicObject player)
    {
        _cinemaCamera.Follow = player.transform;
        _cinemaCamera.LookAt = player.transform;
    }
    private void Bind(bool bind)
    {
        if (bind)
        {
            _spawner.OnPlayerSpawn += OnPlayerSpawn;
        }
        else
        {
            _spawner.OnPlayerSpawn -= OnPlayerSpawn;

        }
    }

    
}
