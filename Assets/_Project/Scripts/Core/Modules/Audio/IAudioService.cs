public interface IAudioService
{
    float MusicVolume { get; }
    float SfxVolume { get; }
    float VoiceVolume { get; }
    void OnEnable();
    void OnDisable();
    void PlayMusic(MusicType type);
    void PlaySfx(SfxType type);
    void PlayVoice(VoiceType type);
}
