using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseModule<GameManager>
{
    public GameObject SceneRoot
    { get; private set; }

    private GameMono mono;


    public override void Init()
    {
        AppContext.managerList = new List<IModule>();
        AppContext.ToBeAdd = new List<IModule>();
        base.Init();

        SceneRoot = new GameObject("SceneRoot");
        SceneRoot.ResetTransform(transform);

        mono = gameobject.AddComponent<GameMono>();
        UnityEngine.Object.DontDestroyOnLoad(gameobject);
    }



    public GameManager StartCoroutine(IEnumerator routine, out Coroutine coroutine)
    {
        coroutine = mono.StartCoroutine(routine);
        return this;
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return mono.StartCoroutine(routine);
    }

    public GameManager StopCoroutine(Coroutine coroutine)
    {
        mono.StopCoroutine(coroutine);
        return this;
    }

    public void Invoke(string methodName, float time)
    {
        mono.Invoke(methodName, time);
    }

    public void CancelInvoke(string methodName = null)
    {
        if (string.IsNullOrEmpty(methodName))
        {
            mono.CancelInvoke();
        }
        else
        {
            mono.CancelInvoke(methodName);
        }

    }

    public GameManager StopAllCroutine()
    {
        mono.StopAllCoroutines();
        return this;
    }

    public GameManager SetFrameRate(int f)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = f;
        return this;
    }

    public void UnInit()
    {
        foreach (var item in AppContext.managerList)
        {
            item.Dispose();
        }
        AppContext.managerList.Clear();
    }

    private class GameMono : MonoBehaviour
    {
        private void Update()
        {
            if (AppContext.ToBeAdd.Count > 0)
            {
                AppContext.managerList.AddRange(AppContext.ToBeAdd);
                AppContext.ToBeAdd.Clear();
            }
            foreach (var item in AppContext.managerList)
            {
                item.Update();
            }

        }

        private void OnApplicationQuit()
        {
            Instance.UnInit();
        }
        private void OnDestroy()
        {
            Instance.UnInit();
        }
    }
}
