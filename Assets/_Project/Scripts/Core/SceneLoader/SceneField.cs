using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SceneField
{
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif

    [SerializeField] private string _sceneName;

    [Button("Update SceneName")]
    public void UpdateSceneName()
    {
        _sceneName = _sceneAsset.name;
    }



    public static implicit operator string(SceneField sceneField)
    {
        return sceneField._sceneName;
    }
}
