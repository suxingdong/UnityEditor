/*==============================================================================
* 文件名:     CustomWindow 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 2:37:13 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEngine;
using System.Collections;
using UnityEditor;

public class CameraExtension : Editor
{

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("Button"))
        {

        }
    }
}

[CanEditMultipleObjects()]
[CustomEditor(typeof(Camera), true)]
public class CustomExtension : CameraExtension
{

}
