using UnityEngine;

public class GameFinisher
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log($"{nameof(GameFinisher)} Stopped Play Mode in Editor");
#else
        Application.Quit();
        Debug.Log($"{nameof(GameFinisher)} Quit called (build)");
#endif
    }
}
