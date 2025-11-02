using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().AsSingle();
    }
}
