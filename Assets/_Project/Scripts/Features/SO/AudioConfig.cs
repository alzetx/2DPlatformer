using System;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "Audio Settings", menuName = "Config/Audio Settings")]
public class AudioConfig : ConfigScriptableObject
{
    [field: SerializeField]
    public SfxDictionary Sfx { get; private set; }
    [field: SerializeField]
    public MusicDictionary Music { get; private set; }
    [field: SerializeField]
    public VoiceDictionary Voice { get; private set; }
}

[Serializable]
public class SfxDictionary : SerializedDictionary<SfxType, AudioClip> { }

[Serializable]
public class MusicDictionary : SerializedDictionary<MusicType, AudioClip> { }

[Serializable]
public class VoiceDictionary : SerializedDictionary<VoiceType, AudioClip> { }

