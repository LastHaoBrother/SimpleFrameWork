using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatUI : Editor
{
    static ModuleBuildView view;

    [MenuItem("Framework/CreatViewItem")]
    public static void CreatNewModule()
    {
        Rect rect = new Rect(0, 0, 350f, 100f);
        view = EditorWindow.GetWindowWithRect(typeof(ModuleBuildView), rect, true, "CreatViewItem") as ModuleBuildView;
        view.Show();
        if (Selection.gameObjects.Length == 1)
        {
            view.moduleNameTxt = Selection.gameObjects[0].name;
        }
    }



    [MenuItem("Framework/RefreshViewItem")]
    public static void RefreshUIFile()
    {
        if (Selection.gameObjects.Length == 0)
        {
            return;
        }
        if (Selection.gameObjects[0].transform.parent == null || Selection.gameObjects[0].transform.parent.GetComponent<Canvas>() == null)
        {
            EditorUtility.DisplayDialog("Update Fail", "请选择Panel！", "OK");
            return;
        }

        ModuleBuildView.RefreshUI(Selection.gameObjects[0].name, Selection.gameObjects[0], true);
        PrefabUtility.ApplyPrefabInstance(Selection.objects[0] as GameObject, InteractionMode.AutomatedAction);

        EditorUtility.DisplayDialog("完成", "代码刷新完成。", "OK");
        EditorUtility.DisplayDialog("完成", "预制体同步完成。", "OK");
        AssetDatabase.Refresh();
    }

    [MenuItem("Framework/DeleteViewItem")]
    public static void DeleteMoudleFile()
    {
        string prefabPath = Application.dataPath + "/[Project]/Resources/Views/";
        string csPath = Application.dataPath + "/[Project]/Scripts/Views/";
        string prefabExtention = ".prefab";
        //EditorUtility.DisplayDialog("Delete Fail", "！", "OK");

        //先判断Hierarchy选择的prefab  如过不包含此模块  包含此模块
        //判断Project选择prefab 如果包含此模块  
        string moudleName = "-1";
        UnityEngine.Object[] objects = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
        if (Selection.gameObjects.Length > 0)
        {
            moudleName = Selection.gameObjects[0].name;
        }
        else if (objects.Length > 0)
        {
            string path = AssetDatabase.GetAssetPath(objects[0]);
            if (path.EndsWith(prefabExtention))
            {
                moudleName = new FileInfo(path).Name;
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "请选择模块/预制体。", "OK");
                return;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("错误", "请选择模块/预制体。", "OK");
            return;
        }

        //Debug.Log(moudleName);
        //判断选择的是否为模块
        //Debug.Log(/*File.Exists*/(prefabPath + moudleName + prefabExtention));
        //Debug.Log((csPath + moudleName));
        if (File.Exists(prefabPath + moudleName + prefabExtention) && Directory.Exists(csPath + moudleName))
        {
            if (EditorUtility.DisplayDialog("提示", $"确认删除 {moudleName} 模块？", "确认", "取消"))
            {
                File.Delete(prefabPath + moudleName + prefabExtention);
                File.Delete(csPath + moudleName + ".meta");
                Directory.Delete((csPath + moudleName), true);
                EditorUtility.DisplayDialog("完成", "删除成功。", "OK");
                AssetDatabase.Refresh();
            }
        }
        else
        {
            EditorUtility.DisplayDialog("错误", "请选择模块/预制体。", "OK");
        }
    }

   

    //[MenuItem("Framework/Test")]
    //public static void Test()
    //{
    //    //File.Delete("E:/Onew/FrameWork/Framework2021/Assets/[Project]/Scripts/Views/Cube.meta");
    //    //Directory.Delete("E:/Onew/FrameWork/Framework2021/Assets/[Project]/Scripts/Views/Cube", true);
    //    //AssetDatabase.Refresh();
    //    //PrefabUtility.DisconnectPrefabInstance(Selection.gameObjects[0]);
    //}





    //[MenuItem("Framework/CreatModelTreeJson")]
    //public static void CreatJson()
    //{
    //    //if (Selection.gameObjects.Length == 0)
    //    //{
    //    //    EditorUtility.DisplayDialog("Creat Fail", "请选择Model！", "OK");
    //    //    return;
    //    //}
    //    //Transform root = Selection.gameObjects[0].transform;
    //    //ModelTree modelTree = new ModelTree();
    //    //modelTree.ModelTreeItems = new List<ModelTreeItem>();



    //    //for (int i = 0; i < root.childCount; i++)
    //    //{
    //    //    ModelTreeItem modelTreeItem1 = new ModelTreeItem();
    //    //    modelTreeItem1.parent = -1;
    //    //    modelTreeItem1.model = i;
    //    //    modelTreeItem1.childeren = new List<ModelTreeItem>();
    //    //    Transform sysModel = root.GetChild(i);
    //    //    ModelItem sysItem = sysModel.GetComponent<ModelItem>();
    //    //    if (sysItem == null)
    //    //    {
    //    //        sysItem = sysModel.gameObject.AddComponent<ModelItem>();
    //    //    }

    //    //    sysItem.Type = ModelHierarchy.System;
    //    //    sysItem.State = ModelState.Selected;
    //    //    sysItem.Parent = null;
    //    //    sysItem.Children = new List<ModelItem>();

    //    //    for (int j = 0; j < sysModel.childCount; j++)
    //    //    {
    //    //        ModelTreeItem modelTreeItem2 = new ModelTreeItem();
    //    //        modelTreeItem2.parent = i;
    //    //        modelTreeItem2.model = j;
    //    //        modelTreeItem2.childeren = new List<ModelTreeItem>();
    //    //        modelTreeItem1.childeren.Add(modelTreeItem2);
    //    //        Transform classModel = sysModel.GetChild(j);
    //    //        ModelItem classItem = classModel.GetComponent<ModelItem>();
    //    //        if (classItem == null)
    //    //        {
    //    //            classItem = classModel.gameObject.AddComponent<ModelItem>();
    //    //        }

    //    //        classItem.Type = ModelHierarchy.Class;
    //    //        classItem.State = ModelState.Selected;
    //    //        classItem.Parent = sysItem;
    //    //        classItem.Children = new List<ModelItem>();
    //    //        classItem.Parent.Children.Add(classItem);

    //    //        for (int k = 0; k < classModel.childCount; k++)
    //    //        {
    //    //            ModelTreeItem modelTreeItem3 = new ModelTreeItem();
    //    //            modelTreeItem3.parent = j;
    //    //            modelTreeItem3.model = k;
    //    //            modelTreeItem3.childeren = new List<ModelTreeItem>();
    //    //            modelTreeItem2.childeren.Add(modelTreeItem3);

    //    //            Transform structModel = classModel.GetChild(k);
    //    //            ModelItem structItem = structModel.GetComponent<ModelItem>();
    //    //            if (structItem == null)
    //    //            {
    //    //                structItem = structModel.gameObject.AddComponent<ModelItem>();
    //    //            }

    //    //            structItem.Type = ModelHierarchy.Struct;
    //    //            structItem.State = ModelState.Selected;
    //    //            structItem.Parent = classItem;
    //    //            structItem.Children = null;
    //    //            structItem.Parent.Children.Add(structItem);

    //    //        }
    //    //    }

    //    //    modelTree.ModelTreeItems.Add(modelTreeItem1);
    //    //}

    //    //string json = JsonUtility.ToJson(modelTree);

    //    //Utils.FileUtils.WriteFile(json, Application.streamingAssetsPath + "/ModelTree/ModelTreeJson.json", true);
    //    //AssetDatabase.Refresh();
    //}



}



