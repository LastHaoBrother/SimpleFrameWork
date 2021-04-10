using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FrameworkSetting : EditorWindow
{

    [MenuItem("Framework/Setting/ShowConfig")]
    static void Setting()
    {
        var win = GetWindowWithRect(typeof(FrameworkSetting), new Rect(200, 200, 400, 600), true);
        win.titleContent = new GUIContent("Setting");
        win.Show();
    }

    //[MenuItem("Framework/CreatConfigAssets")]
    //static void CreatAssets()
    //{
    //    ScriptableObject asset = ScriptableObject.CreateInstance<FrameworkConfig>();
    //    if (File.Exists(Application.dataPath + "tttt.asset"))
    //    {
    //        Debug.Log("cunzai");
    //    }
    //    else
    //    {
    //        AssetDatabase.CreateAsset(asset, "Assets/tttt.asset");
    //        AssetDatabase.Refresh();
    //    }
    //}


    private int defaultCapacity = 30;
    private List<string> shortNameList;
    private List<string> componentNameList;

    string defaultPath;
    string customPath;
    private void Awake()
    {
        defaultPath = Application.dataPath + @"\[Project]\Scripts\Config\DefaultConfig.txt";
        customPath = Application.dataPath + @"\[Project]\Scripts\Config\CustomConfig.txt";
        InitConfig();
    }




    int removeIndex = -1;
    string defaultValue = "...";
    int id;
    private void OnGUI()
    {
        id = GUILayout.Toolbar(id, new[] { "配置", "使用说明" });
        string title = id == 0 ? "配置" : "使用说明";
        GUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label($"-----------------{title}-----------------");
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(15);

        if (id == 0)
        {
            if (shortNameList == null || shortNameList.Count == 0)
            {
                return;
            }
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < shortNameList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("组件名称:", GUILayout.Width(60));
                componentNameList[i] = EditorGUILayout.TextField(componentNameList[i], GUILayout.Width(120));
                //EditorGUILayout.Space(20);
                EditorGUILayout.LabelField("缩写索引:", GUILayout.Width(60));
                shortNameList[i] = EditorGUILayout.TextField(shortNameList[i], GUILayout.Width(80));
                if (GUILayout.Button("删除"))
                {
                    removeIndex = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("添加"))
            {
                if (shortNameList.Contains(defaultValue))
                {

                }
                else
                {
                    shortNameList.Add(defaultValue);
                    componentNameList.Add(defaultValue);
                }
            }
            //删除
            if (removeIndex >= 0)
            {
                shortNameList.RemoveAt(removeIndex);
                componentNameList.RemoveAt(removeIndex);
                removeIndex = -1;
            }

            EditorGUILayout.EndVertical();


            GUILayout.BeginArea(new Rect(80, position.height - 50, position.width - 160, 50));
            GUILayout.BeginHorizontal();
            bool defaultBtn = GUILayout.Button("恢复默认"/*,GUILayout.Width(70)*/);
            bool saveBtn = GUILayout.Button("保存");
            bool resetBtn = GUILayout.Button("重置本次");
            GUILayout.EndHorizontal();
            GUILayout.EndArea();





            if (defaultBtn)
            {
                LoadConfig(defaultPath);
                File.WriteAllText(customPath, null);
            }
            if (saveBtn)
            {
                if (shortNameList.Contains(defaultValue))
                {
                    shortNameList.Remove(defaultValue);
                    componentNameList.Remove(defaultValue);
                }
                File.WriteAllText(customPath, componentNameList.ParaeString('|') + "," + shortNameList.ParaeString('|'));
            }
            if (resetBtn)
            {
                InitConfig();
            }
        }
        if (id == 1)
        {
            GUI.skin.label.wordWrap = true;
            GUILayout.Label("1.ViewItem分为UI与3D物体，选择CreatUIPanel自动将选择的物体的Layer设置为UI，其他则为默认，UI界面以Panel为单位创建" +
                "，要求父对象为Canvas，3D物体没有限制。脚本调用为:AppContext.vMagager.Show<XXXXPanelView>()或者 ViewManager.Instance.Show<XXXView>();\n\n" +
                "2.生成ViewItem可以根据Setting里面的配置自动生成脚本并获取组件。\n\n" +
                "3.UpdateDefaultConfig功能是为了修改默认配置，修改脚本FrameworkSetting.cs中的UpdateConfig函数后，使用此功能。（使用默认配置会覆盖掉" +
                "自定义配置）");

        }

        GUILayout.BeginArea(new Rect(position.width - 130, position.height - 20, 130, 20));
        GUI.color = Color.grey;
        GUIStyle vStyle = new GUIStyle();
        vStyle.fontStyle = FontStyle.Italic;
        GUILayout.Label(" Version ******** ", vStyle);
        GUILayout.EndArea();
    }



    void InitConfig()
    {
        shortNameList = new List<string>(defaultCapacity);
        componentNameList = new List<string>(defaultCapacity);
        if (string.IsNullOrEmpty(File.ReadAllText(customPath)))
        {
            LoadConfig(defaultPath);
        }
        else
        {
            LoadConfig(customPath);
        }

    }

    void LoadConfig(string path)
    {
        shortNameList.Clear();
        componentNameList.Clear();

        string txt = File.ReadAllText(path);
        string[] strLists = txt.Split(',');
        componentNameList.AddRange(strLists[0].Split('|'));
        shortNameList.AddRange(strLists[1].Split('|'));
    }

    [MenuItem("Framework/Setting/UpdateDefaultConfig")]
    static void UpdateConfig()
    {
        //EditorUtility.DisplayDialog("提示", "从FrameworkSetting.cs修改UpdateConfig函数后使用此功能。", "知道了");

        string defaultPath = Application.dataPath + @"\[Project]\Scripts\Config\DefaultConfig.txt";
        List<string> shortNameList = new List<string>(30);
        List<string> componentNameList = new List<string>(30);


        componentNameList.Add("Text"); shortNameList.Add("_txt");
        componentNameList.Add("Button"); shortNameList.Add("_btn");
        componentNameList.Add("Toggle"); shortNameList.Add("_tge");
        componentNameList.Add("Image"); shortNameList.Add("_img");
        componentNameList.Add("CanvasGroup"); shortNameList.Add("_cg");
        componentNameList.Add("InputField"); shortNameList.Add("_ipt");
        componentNameList.Add("RawImage"); shortNameList.Add("_raw");
        componentNameList.Add("Scrollbar"); shortNameList.Add("_scr");
        componentNameList.Add("Slider"); shortNameList.Add("_sli");
        componentNameList.Add("ToggleGroup"); shortNameList.Add("_tg");
        componentNameList.Add("Dropdown"); shortNameList.Add("_dd");
        componentNameList.Add("RectTransform"); shortNameList.Add("_rt");
        componentNameList.Add("UnityEngine.Video.VideoPlayer"); shortNameList.Add("_vp");
        File.WriteAllText(defaultPath, componentNameList.ParaeString('|') + "," + shortNameList.ParaeString('|'));

        shortNameList.Clear();
        componentNameList.Clear();
    }



    private void OnFocus()
    {
        //InitConfig();
    }
}
