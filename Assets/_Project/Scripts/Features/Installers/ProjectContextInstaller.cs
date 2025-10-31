using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<GameFinisher>().AsSingle();
    }
}
