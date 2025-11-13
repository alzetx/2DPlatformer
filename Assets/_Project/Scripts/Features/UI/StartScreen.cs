using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartScreen : MonoBehaviour, IInitializable, IGameStartListener
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private GameObject _root;
    private GameStateMachine _gameState;

    [Inject]
    private void Construct(GameStateMachine gameState)
    {
        _gameState = gameState;
    }
    public void Initialize()
    {
        _root.SetActive(true);
        _startButton.onClick.AddListener(_gameState.StartGame);

    }

    public void OnStartGame()
    {
        _root.SetActive(false);
        _startButton.onClick.RemoveListener(_gameState.StartGame);
    }
}
