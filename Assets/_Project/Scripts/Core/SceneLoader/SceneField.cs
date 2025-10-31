using System;
using UnityEngine;

[Serializable]
public class SceneField
{
    [SerializeField] private UnityEngine.Object _sceneAsset;

    public string SceneName
    {
        get => _sceneAsset.name;
    }
    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }
}
