using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [Header("Audio")]
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    public AudioConfig _config;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _sfxSource;
    [SerializeField]
    private AudioSource _voiceSource;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<GameFinisher>().AsSingle();
        Container.Bind<IStorageService>().To<JsonToFileStorageService>().AsSingle();
        BindAudio();
    }

    private void BindAudio()
    {
        Container.Bind<IAudioService>()
            .To<AudioManager>()
            .AsSingle()
            .WithArguments(_config, _mixer, _musicSource, _sfxSource, _voiceSource);

        Container.Bind<IAudioSettingsService>()
            .To<AudioSettingsService>()
            .AsSingle();
    }
}
