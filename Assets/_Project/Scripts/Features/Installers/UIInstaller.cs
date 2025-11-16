using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private StartScreen _startScreen;
    [SerializeField]
    private HealthView _healthView;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<StartScreen>().FromInstance(_startScreen);
        Container.BindInstance(_healthView).AsSingle();
    }
}
