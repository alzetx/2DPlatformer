using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private SpriteScroller _scroller;
    [SerializeField]
    private StartScreen _startScreen;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SpriteScroller>().FromInstance(_scroller);
        Container.BindInterfacesTo<StartScreen>().FromInstance(_startScreen);
    }
}
