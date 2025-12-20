using System;
using UnityEngine;

[Serializable]
public class SceneField
{
    [SerializeField, SceneGuidSelector]
    private string _sceneGuid;

    public string SceneGuid => _sceneGuid;

#if UNITY_EDITOR
    public string ScenePath => UnityEditor.AssetDatabase.GUIDToAssetPath(_sceneGuid);
    public string SceneName => System.IO.Path.GetFileNameWithoutExtension(ScenePath);
#endif

    public static implicit operator string(SceneField field)
    {
        return field._sceneGuid;
    }
}
