using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private SceneField _nextScene;
    [SerializeField]
    private MenuUI _ui;
    private SceneLoader _sceneLoader;
    GameFinisher _gameFinisher;

   [Inject]
    private void Construct(SceneLoader sceneLoader, GameFinisher gameFinisher)
    {
        _sceneLoader = sceneLoader;
        _gameFinisher = gameFinisher;
    }

    private void OnEnable()
    {
        Bind(true);
    }

    private void OnDisable()
    {
        Bind(false);
    }
    private void Bind(bool bind)
    {
        if (bind)
        {
            _ui._onClickStart += OnClickStart;
            _ui._onClickExit += _gameFinisher.ExitGame;
        }
        else
        {
            _ui._onClickStart -= OnClickStart;
            _ui._onClickExit -= _gameFinisher.ExitGame;
        }
    }
    private void OnClickStart()
    {
        AudioService audioService = new();
        LoadingStep audiostep = new("first", audioService.InitializeTask);
        var loadingSteps = new List<LoadingStep>
            {
                 new LoadingStep("first", audioService.InitializeTask),
                 new LoadingStep("second", audioService.InitializeTask)
            };


        _sceneLoader.LoadSceneAsync(_nextScene, loadingSteps);
    }
}

