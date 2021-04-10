using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : IView
{
    public bool isShow;
    protected GameObject gameObject;
    protected Transform _Transform => gameObject?.transform;

    public virtual BaseUI CreatGUI()
    {
        Init();
        return this;
    }

    public virtual void Dispose()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
            gameObject = null;

            Resources.UnloadUnusedAssets();
        }

        isShow = false;
    }

    public virtual void Hide()
    {
        if (isShow == false)
        {
            return;
        }
        gameObject.SetActive(false);
        isShow = false;
    }

    public virtual void Init()
    {
        //isShow = true;
    }

    public virtual void Show()
    {
        if (isShow)
        {
            return;
        }
        gameObject.SetActive(true);
        _Transform.SetAsLastSibling();
        isShow = true;
    }

    public virtual void Update()
    {

    }

    protected void Instantiate(string path)
    {
        gameObject = GameObject.Instantiate(Resources.Load<GameObject>(path));
        if (gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            gameObject.ResetTransform(AppContext.vMagager.gameobject.transform);
            gameObject.transform.SetAsLastSibling();
        }
        else
        {
            gameObject.ResetTransform(AppContext.gManager.SceneRoot.transform);
        }

    }


}

interface IView : IModule
{

    void Show();
    void Hide();
    BaseUI CreatGUI();
}
