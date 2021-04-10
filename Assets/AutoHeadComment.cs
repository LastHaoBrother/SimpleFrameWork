using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AutoHeadComment : UnityEditor.AssetModificationProcessor
{

    //string TemplateName = "80-EmptyC# Script-NewSimpleScript.cs";
    public static void OnWillCreateAsset(string metaName)
    {
        //Debug.Log(metaName);
        string filePath = metaName.Replace(".meta", "");
        //string fileExt = Path.GetExtension(filePath);
        if (filePath.EndsWith(".cs"))
        {
            string fileFullPath = Application.dataPath.Replace("Assets", "") + filePath;
            string fileContent = File.ReadAllText(fileFullPath);
            string commentContent =
                               "// ========================================================\r\n"
                             + "// Description：\r\n"
                             + "// Author：#AUTHOR# \r\n"
                             + "// CreateTime：#CreateTime#\r\n"
                             + "// Version：#UNITYVERSION#\r\n"
                             + "// ========================================================\r\n";

            commentContent = commentContent.Replace("#AUTHOR#", Environment.UserName);
            commentContent = commentContent.Replace("#CreateTime#", DateTime.Now.ToString("yyy/MM/dd HH:mm"));
            commentContent = commentContent.Replace("#UNITYVERSION#", Application.unityVersion);

            fileContent = fileContent.Insert(0, commentContent);
            File.WriteAllText(fileFullPath, fileContent);
            AssetDatabase.Refresh();
        }
    }


}
