using Atomic.Objects;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener
{
    [SerializeField] private Character _prefab;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Transform _parent;
    [SerializeField, Range(0f, 10f)] private float _respawnDelay;

    private DiContainer _container;

    private Character _player;
    private HealthData _healthData;

    private Countdown _countdown;
    private Coroutine _countdownRoutine;

    private bool _isPaused;

    public event Action<AtomicObject> OnPlayerSpawn;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _countdown = new Countdown(_respawnDelay, OnCountdownEnded);
    }

    void IGameStartListener.OnStartGame()
    {
        SpawnPlayer();
    }

    void IGamePauseListener.OnPauseGame()
    {
        _isPaused = true;
    }

    void IGameResumeListener.OnResumeGame()
    {
        _isPaused = false;
    }

    private void SpawnPlayer()
    {
        CleanUp();
        StopCountdown();

        if (_player != null)
        {
            DestroyPlayer();
        }

        _player = _container.InstantiatePrefabForComponent<Character>(
            _prefab,
            _pointSpawn.position,
            _pointSpawn.rotation,
            _parent);

        _healthData = _player.Get<HealthData>(GameConstants.Variables.Health);
        _healthData.OnDeathEvent += OnPlayerDied;

        OnPlayerSpawn?.Invoke(_player);
    }

    private void OnPlayerDied()
    {
        _healthData.OnDeathEvent -= OnPlayerDied;

        Destroy(_player.gameObject);
        CleanUp();

        StartCountdown();
    }
    private void CleanUp()
    {
        _player = null;
        _healthData = null;
    }

    private void StartCountdown()
    {
        _countdown.Reset();

        if (_countdownRoutine != null)
            StopCoroutine(_countdownRoutine);

        _countdownRoutine = StartCoroutine(CountdownRoutine());
    }

    private void StopCountdown()
    {
        if (_countdownRoutine != null)
            StopCoroutine(_countdownRoutine);

        _countdownRoutine = null;
    }

    private IEnumerator CountdownRoutine()
    {
        while (_countdown.IsPlaying())
        {
            if (!_isPaused)
            {
                _countdown.Tick(Time.deltaTime);
            }

            yield return null;
        }
    }

    [Button]
    private void DestroyPlayer()
    {
        _healthData.OnDeathEvent -= DestroyPlayer;
        Destroy(_player.gameObject); _countdown.Reset();
        _player = null;
        _healthData = null;
    }

    private void OnCountdownEnded()
    {
        SpawnPlayer();
    }
}
