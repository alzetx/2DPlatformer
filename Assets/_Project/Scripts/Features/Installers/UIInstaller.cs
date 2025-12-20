using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private StartScreen _startScreen;
    [SerializeField]
    private SliderCurrencyView _healthSlider;
    [SerializeField]
    private HPView _healthView;
    [SerializeField]
    private CoinView _coinView;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<StartScreen>().FromInstance(_startScreen);
        Container.BindInstance(_healthSlider).AsSingle();
        Container.BindInstance(_healthView).AsSingle();
        Container.BindInstance(_coinView).AsSingle();
    }
}
