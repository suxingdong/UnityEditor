/*
* ==============================================================================
*
* 文件名:     ScriptTemplet 
* 作者：      #AUTHORNAME#
* 创建时间:   #CREATETIME#
* 公司:       游戏公司
* 版本号:     1.0
* 描述: 
*
* ==============================================================================
*/

using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class ScriptTemplet : UnityEditor.AssetModificationProcessor
{

    public static string Version = "1.0.0";
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.ToLower().EndsWith(".cs") || path.ToLower().EndsWith(".lua"))
        {
            string content = File.ReadAllText(path);

            content = content.Replace("#AUTHORNAME#", "East.Su");
            content = content.Replace("#CREATETIME#", DateTime.Now.Date.ToString());
            content = content.Replace("#COMPANY#", "龙语游戏");
            content = content.Replace("#VERSION#", Version);

            File.WriteAllText(path, content);
        }
    }
}
