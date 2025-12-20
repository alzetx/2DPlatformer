using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : IAudioService
{
    private readonly IAudioSettingsService _settingService;
    private readonly AudioConfig _config;
    private readonly AudioMixer _mixer;

    private readonly AudioSource _musicSource;
    private readonly AudioSource _sfxSource;
    private readonly AudioSource _voiceSource;

    private float _musicVolume = 0.6f;
    private float _sfxVolume = 0.6f;
    private float _voiceVolume = 0.6f;

    private const string MUSIC_PARAM = "MusicVolume";
    private const string SFX_PARAM = "SfxVolume";
    private const string VOICE_PARAM = "VoiceVolume";

    public float MusicVolume => _musicVolume;
    public float SfxVolume => _sfxVolume;
    public float VoiceVolume => _voiceVolume;

    public AudioManager(AudioConfig config, AudioMixer mixer, AudioSource musicSource,
        AudioSource sfxSource, AudioSource voiceSource, IAudioSettingsService setting)
    {
        _config = config;
        _mixer = mixer;

        _musicSource = musicSource;
        _sfxSource = sfxSource;
        _voiceSource = voiceSource;

        _settingService = setting;
    }
    public void OnEnable()
    {
        Bind(true);
    }
    public void OnDisable()
    {
        Bind(false);
    }
    public void Bind(bool bind)
    {
        if (bind)
        {
            _settingService.OnMusicChanged += SetMusicVolume;
            _settingService.OnVoiceChanged += SetVoiceVolume;
            _settingService.OnSfxChanged += SetSfxVolume;
        }
        else
        {
            _settingService.OnMusicChanged -= SetMusicVolume;
            _settingService.OnVoiceChanged -= SetVoiceVolume;
            _settingService.OnSfxChanged -= SetSfxVolume;
        }
    }
    public void PlayMusic(MusicType type)
    {
        if (_config.Music.TryGetValue(type, out var clip))
        {
            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlaySfx(SfxType type)
    {
        if (_config.Sfx.TryGetValue(type, out var clip))
        {
            _sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayVoice(VoiceType type)
    {
        if (_config.Voice.TryGetValue(type, out var clip))
        {
            _voiceSource.PlayOneShot(clip);
        }
    }

    private void SetMusicVolume(float value)
    {
        _musicVolume = value;
        _mixer.SetFloat(MUSIC_PARAM, VolumeToDb(value));
    }

    private void SetSfxVolume(float value)
    {
        _sfxVolume = value;
        _mixer.SetFloat(SFX_PARAM, VolumeToDb(value));
    }

    private void SetVoiceVolume(float value)
    {
        _voiceVolume = value;
        _mixer.SetFloat(VOICE_PARAM, VolumeToDb(value));
    }

    private float VolumeToDb(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
    }
}



