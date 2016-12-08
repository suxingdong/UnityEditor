/*==============================================================================
* 文件名:     ShowImageEditor 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 2:30:19 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ShowImage))]
public class ShowImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShowImage tshowImage = (ShowImage)target;
        tshowImage._rectValue = EditorGUILayout.RectField("图片区域",tshowImage._rectValue);
        tshowImage._texture = EditorGUILayout.ObjectField("贴图", tshowImage._texture, typeof(Texture), true) as Texture;
    }
}
