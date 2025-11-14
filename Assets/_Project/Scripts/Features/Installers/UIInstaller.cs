using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private StartScreen _startScreen;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<StartScreen>().FromInstance(_startScreen);
    }
}
