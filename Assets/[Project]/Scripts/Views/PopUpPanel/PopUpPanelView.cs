using System;
using UnityEngine.Events;

public partial class PopUpPanelView : BaseUI
{
    public override void Init()
    {
        base.Init();
    }
    public override void Show()
    {
        base.Show();

        //Panel1_go.SetActive(false);
        //Panel2_go.SetActive(false);
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void Dispose()
    {
        base.Dispose();
    }

    public void ShowMessage(string title, string msg, string btnOk, UnityAction btnAction = null)
    {
        p1Title_txt.text = title;
        p1Msg_txt.text = msg;
        p1Btn_txt.text = btnOk;

        p1Confirm_btn.onClick.RemoveAllListeners();
        p1Confirm_btn.onClick.AddListener(Hide);
        if (btnAction != null) p1Confirm_btn.onClick.AddListener(btnAction);

        Panel1_go.SetActive(true);
        Panel2_go.SetActive(false);
    }

    public void ShowMessage(string title, string msg, string btn1, string btn2, UnityAction btn1Action = null, UnityAction btn2Action = null)
    {
        p2Title_txt.text = title;
        p2Msg_txt.text = msg;
        p2Confirm_txt.text = btn1;
        p2Cancel_txt.text = btn2;

        p2Confirm_btn.onClick.RemoveAllListeners();
        p2Confirm_btn.onClick.AddListener(Hide);
        if (btn1Action != null) p2Confirm_btn.onClick.AddListener(btn1Action);

        p2Cancel_btn.onClick.RemoveAllListeners();
        p2Cancel_btn.onClick.AddListener(Hide);
        if (btn2Action != null) p2Cancel_btn.onClick.AddListener(btn2Action);

        Panel1_go.SetActive(false);
        Panel2_go.SetActive(true);
    }

}
