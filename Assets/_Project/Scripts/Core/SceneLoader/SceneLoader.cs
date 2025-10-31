using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader :  IDisposable
{
    private const int DELAY_BEFORE_SCENE_ACTIVATION_MS = 2000;
    private const int DELAY_AFTER_PROGRESS_COMPLETE_MS = 1000;
    private ReactiveProperty<bool> _onLoaded = new(false);
    private ReactiveProperty<float> _totalProgress = new(0f);
    private ReactiveProperty<LoadingStep> _nextStep = new(default);
    private AsyncOperation _asyncOperation;
   

    public event Action OnStartLoading;
    public IReactiveProperty<LoadingStep> NextStep => _nextStep;
    public IReactiveProperty<float> TotalProgress => _totalProgress;


    
    public IReactiveProperty<bool> OnLoaded => _onLoaded;

    public void Dispose()
    {
        _onLoaded.Dispose();
        _totalProgress.Dispose();
        _nextStep.Dispose();
    }


    public async Task LoadSceneAsync(string sceneName, List<LoadingStep> qwe = null)
    {
        _onLoaded.Value = false;
        OnStartLoading?.Invoke();
        var loadingSceneStep = new LoadingStep(description: "LoadingScene", actionAsync: async () => await LoadScene(sceneName));
        List<LoadingStep> steps = (qwe);
        steps.Add(loadingSceneStep);

        var stepFraction = 1f / steps.Count;
        _totalProgress.Value = 0;

        foreach (var step in steps)
        {
            _nextStep.Value = step;
            await step.ActionAsync();
            _totalProgress.Value += stepFraction;
        }
        await Task.Delay(DELAY_AFTER_PROGRESS_COMPLETE_MS);
        _onLoaded.Value = true;
        _asyncOperation.allowSceneActivation = true;
    }

    private async Task LoadScene(string sceneLoad)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneLoad);
        _asyncOperation.allowSceneActivation = false;

        while (_asyncOperation.progress < 0.9f)
        {
            await Task.Yield();
        }
        await Task.Yield();
        await Task.Delay(DELAY_BEFORE_SCENE_ACTIVATION_MS);

    }

}