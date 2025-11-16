using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const int DELAY_BEFORE_SCENE_ACTIVATION_MS = 2000;
    private const int DELAY_AFTER_PROGRESS_COMPLETE_MS = 3000;

    private bool _isLoading;
    private float _totalProgress;
    private AsyncOperation _asyncOperation;

    public event Action OnStartLoading;
    public event Action OnLoadingFinished;
    public event Action<float> OnProgressChanged;
    public event Action<LoadingStep> OnStepChanged;

    public async UniTask LoadSceneAsync(string targetScene, List<LoadingStep> customSteps = null)
    {
        if (_isLoading)
            return;

        _isLoading = true;
        OnStartLoading?.Invoke();

        var currentScene = SceneManager.GetActiveScene();

        var loaderScene = await LoadAdditiveScene(GameConstants.SceneNames.AdditiveScene);

        await UnloadSceneSafe(currentScene);

        await ExecuteLoadingSteps(targetScene, customSteps);

        await ActivateAndFinalize(targetScene, loaderScene);

        _isLoading = false;
        OnLoadingFinished?.Invoke();
    }
    private async UniTask UnloadSceneSafe(Scene scene)
    {
        if (scene.isLoaded && scene.name != GameConstants.SceneNames.AdditiveScene)
            await SceneManager.UnloadSceneAsync(scene);
    }

    private async UniTask ExecuteLoadingSteps(string targetScene, List<LoadingStep> customSteps)
    {
        var steps = new List<LoadingStep>();
        if (customSteps != null)
        {
            steps.AddRange(customSteps);
        }

        var loadSceneStep = new LoadingStep("LoadingScene", async () => await LoadTargetScene(targetScene));
        steps.Add(loadSceneStep);
        
        float stepFraction = 1f / steps.Count;
        ChangeProgress(0);

        foreach (var step in steps)
        {
            OnStepChanged?.Invoke(step);
            await step.ActionAsync();
            ChangeProgress(_totalProgress + stepFraction); 
        }

        await UniTask.Delay(DELAY_AFTER_PROGRESS_COMPLETE_MS);
    }

    private async UniTask<Scene> LoadAdditiveScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        var scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        await UniTask.Yield();
        return scene;
    }

    private async UniTask LoadTargetScene(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _asyncOperation.allowSceneActivation = false;


        while (_asyncOperation.progress < 0.9f)
        {
            float sceneRelativeProgress = Mathf.InverseLerp(0f, 0.9f, _asyncOperation.progress);
            await UniTask.Yield();
        }

        await UniTask.Delay(DELAY_BEFORE_SCENE_ACTIVATION_MS);
    }

    private async UniTask ActivateAndFinalize(string targetScene, Scene loaderScene)
    {
        _asyncOperation.allowSceneActivation = true;
        await UniTask.Yield();

        var newScene = SceneManager.GetSceneByName(targetScene);
        SceneManager.SetActiveScene(newScene);

        await SceneManager.UnloadSceneAsync(loaderScene);
    }

    private void ChangeProgress(float value)
    {
        _totalProgress = Mathf.Clamp01(value);
        OnProgressChanged?.Invoke(_totalProgress);
    }
}
