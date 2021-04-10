using UnityEngine;
using UnityEngine.UI;

public class AppVerison : MonoBehaviour
{
    private Text version_txt;
    private Button close_btn;
    private Button version_btn;
    private GameObject version_panel;

    private void Awake()
    {
        version_btn = transform.Find("Version_btn").GetComponent<Button>();
        version_panel = transform.Find("Verison_bg").gameObject;
        version_txt = transform.GetComponentInChildren<Text>(true);
        close_btn = version_panel.transform.GetComponentInChildren<Button>(true);

        version_btn.onClick.AddListener(ShowVersion);
        close_btn.onClick.AddListener(HideVersion);

        version_txt.text = XmlUtils.ReadNode(AppConst.GloableConfigPath, AppConst.VersionNode);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            if (version_panel.activeSelf) HideVersion();
            else ShowVersion();
        }
    }

    private void ShowVersion()
    {
        version_panel.SetActive(true);
    }

    private void HideVersion()
    {
        version_panel.SetActive(false);
    }
}
