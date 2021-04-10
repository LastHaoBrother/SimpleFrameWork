using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : BaseModule<SceneController>
{
    private BaseScene currentScene = null;


    public override void Init()
    {
        base.Init();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        if (currentScene != null && currentScene.SceneName == SceneManager.GetActiveScene().name)
        {
            System.GC.Collect();
            currentScene.OnEnter();
            currentScene.active = true;

        }
    }

    public void LoadScene<T>() where T : BaseScene, new()
    {
        T t = new T();
        if (currentScene != null && t.SceneName == currentScene.SceneName)
        {
            Debug.Log($"当前已经是 {t.SceneName} 场景");
            return;
        }
        if (currentScene != null)
        {
            currentScene.active = false;
            currentScene.OnLeave();
        }

        currentScene = t;
        SceneManager.LoadScene(currentScene.SceneName);
    }


}
