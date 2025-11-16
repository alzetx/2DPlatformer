using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BootsTrap : MonoBehaviour
{
    [SerializeField]
    private SceneField _nextScene;

    private SceneLoader _sceneLoader;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }


    private async void Awake()
    {
        var loadingSteps = new List<LoadingStep>();
        AudioService audioService = new(); 
        LoadingStep audiostep = new(audioService.InitializeTask);

        loadingSteps.Add(audiostep);
        await LoadScene(loadingSteps);
        Debug.Log("All loaded");
    }

    private async Task LoadScene(List<LoadingStep> loadingSteps)
    {
        await _sceneLoader.LoadSceneAsync(_nextScene, loadingSteps);
    }

}