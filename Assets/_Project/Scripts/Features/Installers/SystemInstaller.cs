using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField]
    private CameraTargetController _cameraController;
    [SerializeField]
    private PlayerSpawner _playerSpawner;
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().AsSingle();
        Container.BindInterfacesTo<CameraTargetController>().FromInstance(_cameraController);
        Container.BindInstance(_playerSpawner).AsSingle();
        Container.BindInterfacesTo<PlayerSpawner>().FromInstance(_playerSpawner);
    }
}
