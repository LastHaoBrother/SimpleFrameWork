using UnityEngine;

public static class AppConst
{
    public static Language DefaultLanguage = Language.Chinese;

    public static string GloableConfigPath = Application.streamingAssetsPath + "/Config/GlobalConfig.xml";

    public static string VersionNode = "VersionNum";

    public static Vector2 ScreenSize = new Vector2(1920, 1080);

}
