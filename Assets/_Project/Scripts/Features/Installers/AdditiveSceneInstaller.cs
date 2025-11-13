using UnityEngine;
using Zenject;

public class AdditiveSceneInstaller : MonoInstaller
{
    [SerializeField]
    private SceneLoaderVisual _visual;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SceneLoaderVisual>().FromInstance(_visual).NonLazy();
    }
}
