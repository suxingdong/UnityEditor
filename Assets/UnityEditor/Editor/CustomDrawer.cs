/*==============================================================================
* 文件名:     CustomDrawer 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 3:24:36 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomPropertyDrawer(typeof(AttributeStruct))]
public class CustomDrawer : PropertyDrawer
{
    public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
    {
        AttributeStruct attribute = (AttributeStruct)base.attribute;
        property.intValue = Mathf.Min(Mathf.Max(EditorGUI.IntField(position, label.text, property.intValue), attribute.min), attribute.max);
        //EditorGUI.Slider(position, property, attribute.min, attribute.max, label);
        EditorGUI.LabelField(position, "00000");
    }
}

