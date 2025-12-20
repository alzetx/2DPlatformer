using System;
using UnityEngine;

class AudioSettingsService : IAudioSettingsService
{
    private const string KEY = "audio_settings.json";

    private readonly IStorageService _storage;
    private AudioData _data;

    public event Action<float> OnMusicChanged;
    public event Action<float> OnSfxChanged;
    public event Action<float> OnVoiceChanged;


    public float MusicVolume => _data.MusicVolume;
    public float SfxVolume => _data.SfxVolume;
    public float VoiceVolume => _data.VoiceVolume;

    public AudioSettingsService(IStorageService storage)
    {
        _storage = storage;
    }

    public void Load()
    {
        _storage.Load<AudioData>(KEY, data => _data = data,
            () => new AudioData()
            {
                MusicVolume = 0.8f,
                SfxVolume = 1f,
                VoiceVolume = 1f
            });
    }

    public void Save()
    {
        _storage.Save(KEY, _data);
    }

    public void SetMusicVolume(float value)
    {
        _data.MusicVolume = value;
        OnMusicChanged?.Invoke(value);
        Save();
    }

    public void SetSfxVolume(float value)
    {
        _data.SfxVolume = value;
        OnSfxChanged?.Invoke(value);
        Save();
    }

    public void SetVoiceVolume(float value)
    {
        _data.VoiceVolume = value;
        OnVoiceChanged?.Invoke(value);
        Save();
    }
}
