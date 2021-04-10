using UnityEditor;

[CustomEditor(typeof(MainCilent))]
public class MainCilentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MainCilent cilent = (MainCilent)target;

        cilent.ShowFPS = EditorGUILayout.Toggle("ShowFps", cilent.ShowFPS);
        cilent.AutoSize = EditorGUILayout.Toggle("AutoSize", cilent.AutoSize);
        if (!cilent.AutoSize)
        {
            cilent.ScreenSize = EditorGUILayout.Vector2Field("ScreenSize", cilent.ScreenSize);
        }
    }
}
