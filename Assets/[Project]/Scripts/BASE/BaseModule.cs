using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModule<T> : IModule where T : BaseModule<T>, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    protected GameObject _gameobject;
    public GameObject gameobject => _gameobject;

    public Transform transform => gameobject?.transform;

    private Dictionary<int, Coroutine> _coroutineDic;


    public BaseModule()
    {
        _gameobject = new GameObject(GetType().Name);
        _gameobject.transform.SetParent(null);
        _gameobject.ResetTransform();
        _coroutineDic = new Dictionary<int, Coroutine>();

        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        AppContext.ToBeAdd.Add(this);
    }






    /// <summary>
    /// 更新模块
    /// </summary>
    public virtual void Update()
    {

    }



    /// <summary>
    /// 释放模块
    /// </summary>
    public virtual void Dispose()
    {

    }

}

public interface IModule
{
    void Init();
    void Update();
    void Dispose();

}
