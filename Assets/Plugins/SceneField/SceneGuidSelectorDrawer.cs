#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class SceneGuidSelectorDrawer : OdinAttributeDrawer<SceneGuidSelectorAttribute, string>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var scenes = EditorBuildSettings.scenes;

        string[] guids = new string[scenes.Length];
        string[] names = new string[scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            string path = scenes[i].path;
            guids[i] = AssetDatabase.AssetPathToGUID(path);
            names[i] = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        string currentGuid = this.ValueEntry.SmartValue;

        int index = System.Array.IndexOf(guids, currentGuid);
        if (index < 0) index = 0;

        int newIndex = EditorGUILayout.Popup(label, index, names);

        this.ValueEntry.SmartValue = guids[newIndex];
    }
}
#endif
