using UnityEngine;

public class MainCilent : MonoBehaviour
{
    public bool ShowFPS;
    public bool AutoSize;
    public Vector2 ScreenSize;



    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        RenderSettings.ambientLight = Color.white;
        Input.multiTouchEnabled = true;
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (AutoSize)
        {
#if UNITY_EDITOR
            GetGameViewSize(out AppConst.ScreenSize.x, out AppConst.ScreenSize.y);
#else
            Resolution[] resolutions = Screen.resolutions;
            AppConst.SceenSize = new Vector2(resolutions[0].height, resolutions[0].width);
#endif
        }
        else
        {
            if (ScreenSize == Vector2.zero)
            {
                Debug.LogError("未设置默认分辨率");
                return;
            }
            AppConst.ScreenSize = ScreenSize;
        }
        Screen.SetResolution((int)AppConst.ScreenSize.x, (int)AppConst.ScreenSize.y, true);
        //QualitySettings.SetQualityLevel(4);
        //QualitySettings.antiAliasing = 4;



        AppContext.gManager.SetFrameRate(60);
        AppContext.lanManager.Init();
        AppContext.sceneManager.LoadScene<GameScene>();

        if (ShowFPS)
        {
            AppContext.gManager.gameobject.AddComponent<FPS>();
        }
    }


#if UNITY_EDITOR
    /// <summary>
    /// 获取Game View的分辨率
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void GetGameViewSize(out float width, out float height)
    {
        var mouseOverWindow = UnityEditor.EditorWindow.mouseOverWindow;
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.PlayModeView");

        Vector2 size = (Vector2)type.GetMethod(
                                          "GetMainPlayModeViewTargetSize",
                                          System.Reflection.BindingFlags.NonPublic |
                                          System.Reflection.BindingFlags.Static
                                      ).
                                      Invoke(mouseOverWindow, null);
        height = size.y;
        width = size.x;
    }
#endif
}
