/*==============================================================================
* 文件名:     OpenRes 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 3:50:20 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class OpenRes
{
    [OnOpenAssetAttribute(1)]
    public static bool step1(int instanceID, int line)
    {
        //string name = EditorUtility.InstanceIDToObject(instanceID).name;
        // Debug.Log("Open Asset step: 1 (" + name + ")");
        return false; // we did not handle the open
    }

    // step2 has an attribute with index 2, so will be called after step1
    [OnOpenAssetAttribute(2)]
    public static bool step2(int instanceID, int line)
    {

        string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        string name = Application.dataPath + "/" + path.Replace("Assets/", "");

        if (name.EndsWith(".txt") || name.EndsWith(".lua"))
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "D:/Program Files/Sublime Text 3/sublime_text.exe";
            startInfo.Arguments = name;
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }
        return false; // we did not handle the open
    }
}


