using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : BaseModule<ViewManager>
{

    public Canvas _Canvas { get; private set; } = null;



    private void CreatCanvas()
    {
        _gameobject.name = "CanvasRoot";
        RectTransform rect = gameobject.AddComponent<RectTransform>();
        gameobject.transform.SetParent(AppContext.gManager.transform);
        rect.anchoredPosition3D = Vector3.zero;
        rect.sizeDelta = AppConst.ScreenSize;
        rect.localScale = Vector3.one;
        _Canvas = gameobject.AddComponent<Canvas>();
        gameobject.AddComponent<GraphicRaycaster>();
        _Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = gameobject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = AppConst.ScreenSize;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 1;
        gameobject.layer = LayerMask.NameToLayer("UI");

    }


    public override void Init()
    {
        base.Init();
        panelList = new List<BaseUI>();
        toBeAddPanel = new List<BaseUI>();
        toBeRemovePanel = new List<BaseUI>();
        CreatCanvas();

    }
    private void UnInit()
    {
        foreach (var item in panelList)
        {
            item.Dispose();
        }
        panelList.Clear();
        toBeAddPanel.Clear();
        toBeRemovePanel.Clear();
    }

    public override void Update()
    {
        base.Update();
        foreach (var item in panelList)
        {
            if (item.isShow)
            {
                item.Update();
            }
        }
        if (toBeAddPanel.Count > 0)
        {
            panelList.AddRange(toBeAddPanel);
            toBeAddPanel.Clear();
        }
        if (toBeRemovePanel.Count > 0)
        {
            foreach (var item in toBeRemovePanel)
            {
                panelList.Remove(item);
            }
            toBeRemovePanel.Clear();
        }
    }
    public override void Dispose()
    {
        base.Dispose();
        UnInit();
    }

    private List<BaseUI> toBeAddPanel;
    private List<BaseUI> toBeRemovePanel;
    #region 接口
    private List<BaseUI> panelList = null;
    public T Show<T>() where T : BaseUI, new()
    {
        BaseUI panel = null;
        foreach (var item in panelList)
        {
            if (item is T)
            {
                panel = item;
                break;
            }
        }
        foreach (var item in toBeAddPanel)
        {
            if (item is T)
            {
                panel = item;
                break;
            }
        }
        if (panel == null)
        {
            panel = new T();
            panel = panel.CreatGUI();
            toBeAddPanel.Add(panel);
        }
        panel.Show();

        return panel as T;

    }

    public void Hide<T>() where T : BaseUI, new()
    {
        BaseUI panel = null;
        foreach (var item in panelList)
        {
            if (item is T)
            {
                panel = item;
                break;
            }
        }
        panel?.Hide();
    }

    public void Destroy<T>() where T : BaseUI, new()
    {
        BaseUI panel = null;
        foreach (var item in panelList)
        {
            if (item is T)
            {
                panel = item;
                break;
            }
        }

        if (panel == null)
        {

        }
        else
        {
            panel.Dispose();
            toBeRemovePanel.Add(panel);
        }
    }
    #endregion
}
