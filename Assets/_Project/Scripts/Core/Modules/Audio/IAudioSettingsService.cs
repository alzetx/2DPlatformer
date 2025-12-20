using System;

public interface IAudioSettingsService
{
    event Action<float> OnMusicChanged;
    event Action<float> OnSfxChanged;
    event Action<float> OnVoiceChanged;
    void Load();
    void Save();

    float MusicVolume { get; }
    float SfxVolume { get; }
    float VoiceVolume { get; }

    void SetMusicVolume(float value);
    void SetSfxVolume(float value);
    void SetVoiceVolume(float value);
}