using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private SpriteScroller _scroller;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SpriteScroller>().FromInstance(_scroller).AsSingle();
    }
}
