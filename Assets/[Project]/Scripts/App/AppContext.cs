using System.Collections.Generic;



public static class AppContext
{
    public static List<IModule> managerList;
    public static List<IModule> ToBeAdd;
    public static GameManager gManager => GameManager.Instance;
    public static ViewManager vMagager => ViewManager.Instance;

    public static LanguageManager lanManager => LanguageManager.Instance;

    public static MsgManager msgManager => MsgManager.Instance;

    public static CommandManager comManager => CommandManager.Instance;

    public static SceneController sceneManager => SceneController.Instance;

}


