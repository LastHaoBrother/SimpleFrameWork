using UnityEngine;
using UnityEngine.UI;

public partial class PopUpPanelView : BaseUI
{
    public GameObject Panel1_go;
    public Text p1Title_txt;
    public Text p1Msg_txt;
    public Button p1Confirm_btn;
    public Text p1Btn_txt;
    public GameObject Panel2_go;
    public Text p2Title_txt;
    public Text p2Msg_txt;
    public Button p2Confirm_btn;
    public Text p2Confirm_txt;
    public Button p2Cancel_btn;
    public Text p2Cancel_txt;

    public PopUpPanelView()
    {
    }
    public override BaseUI CreatGUI()
    {
        Instantiate("Views/PopUpPanel");
        Panel1_go = gameObject.transform.Find("Panel1_go").gameObject;
        p1Title_txt = gameObject.transform.Find("Panel1_go/bg/p1Title_txt").GetComponent<Text>();
        p1Msg_txt = gameObject.transform.Find("Panel1_go/bg/p1Msg_txt").GetComponent<Text>();
        p1Confirm_btn = gameObject.transform.Find("Panel1_go/bg/p1Confirm_btn").GetComponent<Button>();
        p1Btn_txt = gameObject.transform.Find("Panel1_go/bg/p1Confirm_btn/p1Btn_txt").GetComponent<Text>();
        Panel2_go = gameObject.transform.Find("Panel2_go").gameObject;
        p2Title_txt = gameObject.transform.Find("Panel2_go/bg/p2Title_txt").GetComponent<Text>();
        p2Msg_txt = gameObject.transform.Find("Panel2_go/bg/p2Msg_txt").GetComponent<Text>();
        p2Confirm_btn = gameObject.transform.Find("Panel2_go/bg/p2Confirm_btn").GetComponent<Button>();
        p2Confirm_txt = gameObject.transform.Find("Panel2_go/bg/p2Confirm_btn/p2Confirm_txt").GetComponent<Text>();
        p2Cancel_btn = gameObject.transform.Find("Panel2_go/bg/p2Cancel_btn").GetComponent<Button>();
        p2Cancel_txt = gameObject.transform.Find("Panel2_go/bg/p2Cancel_btn/p2Cancel_txt").GetComponent<Text>();
        return base.CreatGUI();
    }
}
