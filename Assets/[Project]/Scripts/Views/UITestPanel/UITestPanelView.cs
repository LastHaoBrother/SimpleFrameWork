using UnityEngine;

public partial class UITestPanelView : BaseUI
{
    public override void Init()
    {
        base.Init();
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void Dispose()
    {
        base.Dispose();
    }

    public override void Update()
    {
        base.Update();


        if (Input.GetKeyDown(KeyCode.A))
        {
            AppContext.vMagager.Show<PopUpPanelView>().ShowMessage("弹窗1a", "测试弹窗1a", "确认a", () => { Debug.Log("点击确认a按钮"); });
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AppContext.vMagager.Show<PopUpPanelView>().ShowMessage("弹窗1s", "测试弹窗1s", "确认s", () => { Debug.Log("点击确认s按钮"); });
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AppContext.vMagager.Show<PopUpPanelView>().ShowMessage("弹窗2d", "测试弹窗2d", "确认d", "取消d", () => { Debug.Log("点击确认d按钮"); }, () => { Debug.Log("点击取消d按钮"); });
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AppContext.vMagager.Show<PopUpPanelView>().ShowMessage("弹窗2f", "测试弹窗2f", "确认f", "取消f", () => { Debug.Log("点击确认f按钮"); }, () => { Debug.Log("点击取消f按钮"); });
        }
    }
}
