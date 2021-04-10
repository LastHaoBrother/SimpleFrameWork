
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Utils;

public class ModuleBuildView : EditorWindow
{
    public string moduleNameTxt;

    private void OnGUI()
    {
        GUI.SetNextControlName("name");
        GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Height(20f) };
        moduleNameTxt = EditorGUILayout.TextField("ViewItemName：", moduleNameTxt, options);


        GUILayoutOption[] btnOptions = new GUILayoutOption[] { GUILayout.Width(200f) };
        if (GUILayout.Button("CreatUIPanel", btnOptions))
        {

            if (Selection.gameObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("创建失败", "请先选择一个模块！", "OK");
                return;

            }
            if (Selection.gameObjects[0].transform.parent == null || Selection.gameObjects[0].transform.parent.GetComponent<Canvas>() == null)
            {
                EditorUtility.DisplayDialog("创建失败", "请以Panel为单位创建UI模块！", "OK");
                return;
            }
            string modulePath = Application.dataPath + "/[Project]/Scripts/Views/" + moduleNameTxt;

            CreatPrefab(moduleNameTxt);
            CreatModuleFile(moduleNameTxt, modulePath);
            Thread.Sleep(100);
            AssetDatabase.Refresh();
            Debug.Log("创建了 ： " + moduleNameTxt);
            base.Close();
        }
        else if (GUILayout.Button("CreatViewItem", btnOptions))
        {
            if (Selection.gameObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("创建失败", "请先选择一个模块！", "OK");
                return;

            }

            string modulePath = Application.dataPath + "/[Project]/Scripts/Views/" + moduleNameTxt;

            CreatPrefab(moduleNameTxt);
            CreatModuleFile(moduleNameTxt, modulePath, false);
            Thread.Sleep(100);
            AssetDatabase.Refresh();
            Debug.Log("创建了 ： " + moduleNameTxt);
            base.Close();
        }

    }

    private void CreatPrefab(string text)
    {
        string path = $"{Application.dataPath}/[Project]/Resources/Views/{text}.prefab";
        if (Selection.objects.Length == 1)
        {
            //PrefabUtility.CreatePrefab(path, Selection.objects[0] as GameObject);
            PrefabUtility.SaveAsPrefabAssetAndConnect(Selection.objects[0] as GameObject, path, InteractionMode.AutomatedAction);
        }
    }

    public static void CreatModuleFile(string uiName, string path, bool withField = true)
    {
        //path = path + "/" + uiName;
        GameObject root = Selection.objects[0] as GameObject;

        FileUtils.CreatDirectory(path);
        //Thread.Sleep(100);
        RefreshUI(uiName, root, false);
        CreatWinClass(uiName, path);
        CreatViewBaseClass(uiName, path, withField);
        //CreatWinProxy(uiName, path);

        EditorUtility.DisplayDialog("完成", "生成代码完成。", "OK");
        AssetDatabase.Refresh();


    }

    private static void CreatWinClass(string winName, string path)
    {
        path = path + "/" + winName + "View.cs";
        winName = winName + "View";
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"///GeneratedTime: {System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        builder.AppendLine("using UnityEngine;");
        //builder.AppendLine("using Frameworks;");
        builder.AppendLine();
        builder.AppendLine("public partial class " + winName + " : BaseUI");
        builder.AppendLine("{");
        AppendFun(builder, "Init", "public");
        AppendFun(builder, "Show", "public");
        AppendFun(builder, "Hide", "public");
        AppendFun(builder, "Dispose", "public");
        builder.AppendLine("}");
        FileUtils.WriteFile(builder.ToString(), path, false);
    }

    private static void CreatViewBaseClass(string winName, string path, bool withField = true)
    {
        path = path + "/" + winName + "ViewBase.cs";
        string str = winName;
        winName = winName + "View";
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"///GeneratedTime: {System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        builder.AppendLine("using UnityEngine;");
        builder.AppendLine("using UnityEngine.UI;");
        //builder.AppendLine("using TMPro;");
        //builder.AppendLine("using Frameworks;");
        builder.AppendLine();
        builder.AppendLine("public partial class " + winName + " : BaseUI");
        builder.AppendLine("{");
        if (withField)
        {
            for (int i = 0; i < types.Count; i++)
            {
                builder.AppendLine(types[i]);
            }
        }

        builder.AppendLine();
        builder.AppendLine("    public " + winName + "()");
        builder.AppendLine("    {");
        //builder.AppendLine("        _id = GUI_ID." + str + ";");
        builder.AppendLine("    }");
        builder.AppendLine("    public override BaseUI CreatGUI()");
        builder.AppendLine("    {");
        builder.AppendLine("        Instantiate(\"Views/" + str + "\");");
        if (withField)
        {
            for (int j = 0; j < types.Count; j++)
            {
                builder.AppendLine(inits[j]);
            }
        }

        builder.AppendLine("        return base.CreatGUI();");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        FileUtils.WriteFile(builder.ToString(), path, true);
    }

    private static void CreatWinProxy(string winName, string path)
    {
        path = path + "/" + winName + "Data.cs";
        winName = winName + "Win";
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("using UnityEngine;");
        //builder.AppendLine("using Frameworks;");
        builder.AppendLine();
        builder.AppendLine("public class " + winName + "Proxy  {");
        //builder.AppendLine("    public static " + winName + "Proxy instance");
        //builder.AppendLine("    {");
        //builder.AppendLine("        get { return Singleton<" + winName + "Proxy>.GetInstance(); }");
        //builder.AppendLine("    }");
        //builder.AppendLine("    public override void InitEvent()");
        //builder.AppendLine("    {");
        //builder.AppendLine("        base.InitEvent();");
        //builder.AppendLine("    }");
        builder.AppendLine("}");
        FileUtils.WriteFile(builder.ToString(), path, true);
    }

    private static void AppendFun(StringBuilder builder, string funName, string nameSpace = "public")
    {
        builder.AppendLine("    " + nameSpace + " override void " + funName + "()");
        builder.AppendLine("    {");
        builder.AppendLine("        base." + funName + "();");
        builder.AppendLine("    }");
    }


    private static List<string> inits = new List<string>();
    private static List<string> types = new List<string>();

    private static void ParseGameObject(GameObject g, bool isClude = true, string pLayer = "")
    {
        int childCount = g.transform.childCount;
        if (isClude)
        {
            if (pLayer == "root")
            {
                AddNewGameObject1(g, g.name);
            }
            else
            {
                AddNewGameObject1(g, $"{pLayer}/{g.name}");
            }
        }
        for (int i = 0; i < childCount; i++)
        {
            if (pLayer == "")
            {
                ParseGameObject(g.transform.GetChild(i).gameObject, true, "root");
            }
            else if (pLayer == "root")
            {
                ParseGameObject(g.transform.GetChild(i).gameObject, true, g.name);
            }
            else
            {
                ParseGameObject(g.transform.GetChild(i).gameObject, true, $"{pLayer}/{g.name}");
            }
        }
    }

    private static void AddNewGameObject1(GameObject obj, string findPath)
    {
        int defaultCapacity = 30;
        List<string> shortNameList = new List<string>(defaultCapacity);
        List<string> componentNameList = new List<string>(defaultCapacity);
        string defaultPath = Application.dataPath + @"\[Project]\Scripts\Config\DefaultConfig.txt";
        string customPath = Application.dataPath + @"\[Project]\Scripts\Config\CustomConfig.txt";
        string txt;
        if (string.IsNullOrEmpty(File.ReadAllText(customPath)))
        {
            txt = File.ReadAllText(defaultPath);
        }
        else
        {
            txt = File.ReadAllText(customPath);
        }
        string[] strLists = txt.Split(',');
        componentNameList.AddRange(strLists[0].Split('|'));
        shortNameList.AddRange(strLists[1].Split('|'));

        if (obj.name.Contains("_"))
        {
            string str = obj.name.ToLower();
            string str2 = "GameObject";
            string str3 = "gameObject";
            for (int i = 0; i < shortNameList.Count; i++)
            {
                if (str.Contains(shortNameList[i]))
                {
                    str2 = componentNameList[i];
                }
            }

            if (str2 != "GameObject")
            {
                str3 = "GetComponent<" + str2 + ">()";
            }
            types.Add("    public " + str2 + " " + obj.name + ";");
            inits.Add("        " + obj.name + " = gameObject.transform.Find(\"" + findPath + "\")." + str3 + ";");
        }
    }

    //private static void AddNewGameObject(GameObject obj, string findPath)
    //{
    //    if (obj.name.Contains("_"))
    //    {
    //        string str = obj.name.ToLower();
    //        string str2 = "GameObject";
    //        string str3 = "gameObject";
    //        if (str.Contains("_txt") || str.Contains("_text"))
    //        {
    //            str2 = "Text";
    //        }
    //        else if (str.Contains("_cg"))
    //        {
    //            str2 = "CanvasGroup";
    //        }
    //        else if (str.Contains("_btn") || str.Contains("_button"))
    //        {
    //            str2 = "Button";
    //        }
    //        else if (str.Contains("_input") || str.Contains("_inputfield"))
    //        {
    //            str2 = "InputField";
    //        }
    //        else if (str.Contains("_img") || str.Contains("_image"))
    //        {
    //            str2 = "Image";
    //        }
    //        else if (str.Contains("_rawimg") || str.Contains("_rawimage"))
    //        {
    //            str2 = "RawImage";
    //        }
    //        else if (str.Contains("_scrollbar"))
    //        {
    //            str2 = "Scrollbar";
    //        }
    //        else if (str.Contains("_slider"))
    //        {
    //            str2 = "Slider";
    //        }
    //        else if (str.Contains("_togglegroup"))
    //        {
    //            str2 = "ToggleGroup";
    //        }
    //        else if (str.Contains("_toggle"))
    //        {
    //            str2 = "Toggle";
    //        }
    //        else if (str.Contains("_dropdown"))
    //        {
    //            str2 = "Dropdown";
    //        }
    //        else if (str.Contains("_outline"))
    //        {
    //            str2 = "Outline";
    //        }
    //        else if (str.Contains("_rt"))
    //        {
    //            str2 = "RectTransform";
    //        }
    //        else if (str.Contains("_csf"))
    //        {
    //            str2 = "ContentSizeFitter";
    //        }
    //        else if (str.Contains("_hlg"))
    //        {
    //            str2 = "HorizontalLayoutGroup";
    //        }
    //        else if (str.Contains("_vlg"))
    //        {
    //            str2 = "VerticalLayoutGroup";
    //        }
    //        else if (str.Contains("_glg"))
    //        {
    //            str2 = "GridLayoutGroup";
    //        }
    //        else if (str.Contains("_video"))
    //        {
    //            str2 = "UnityEngine.Video.VideoPlayer";
    //        }
    //        else if (str.Contains("_mesh"))
    //        {
    //            str2 = "MeshRender";
    //        }
    //        else if (str.Contains("_clip"))
    //        {
    //            str2 = "Clipping";
    //        }
    //        else if (str.Contains("_ani"))
    //        {
    //            str2 = "Animation";
    //        }

    //        if (str2 != "GameObject")
    //        {
    //            str3 = "GetComponent<" + str2 + ">()";
    //        }
    //        types.Add("    public " + str2 + " " + obj.name + ";");
    //        inits.Add("        " + obj.name + " = gameObject.transform.Find(\"" + findPath + "\")." + str3 + ";");
    //    }
    //}

    public static void RefreshUI(string uiName, GameObject root, bool refresh = true)
    {
        types.Clear();
        inits.Clear();
        if (root != null)
        {
            ParseGameObject(root, false, "");
        }

        CreatViewBaseClass(uiName, Application.dataPath + "/[Project]/Scripts/Views/" + uiName);



    }
}
