using UnityEngine;
using UnityEngine.UI;

public partial class UITestPanelView : BaseUI
{
    public Text ss_txt;
    public Button Version_btn;
    public GameObject Verison_bg;
    public Text Version_txt;
    public Button VersionClose_btn;

    public UITestPanelView()
    {
    }
    public override BaseUI CreatGUI()
    {
        Instantiate("Views/UITestPanel");
        ss_txt = gameObject.transform.Find("ss_txt").GetComponent<Text>();
        Version_btn = gameObject.transform.Find("VersionPanel/Version_btn").GetComponent<Button>();
        Verison_bg = gameObject.transform.Find("VersionPanel/Verison_bg").gameObject;
        Version_txt = gameObject.transform.Find("VersionPanel/Verison_bg/Version_txt").GetComponent<Text>();
        VersionClose_btn = gameObject.transform.Find("VersionPanel/Verison_bg/VersionClose_btn").GetComponent<Button>();
        return base.CreatGUI();
    }
}
