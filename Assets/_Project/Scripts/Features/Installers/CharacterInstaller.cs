using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField]
    private Character _character;


    public override void InstallBindings()
    {
        _character.Compose();
        Container.BindInterfacesTo<Character>().FromInstance(_character);
        Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle();
        Container.BindInterfacesTo<CharacterInputBinder>().AsSingle().WithArguments(_character);


    }

}
