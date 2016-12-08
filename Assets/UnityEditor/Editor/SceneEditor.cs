/*==============================================================================
* 文件名:     SceneEditor 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 2:44:46 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEditor;
using UnityEngine;

//自定义Tset脚本
[CustomEditor(typeof(SceneTest))]
//请继承Editor
public class SceneEditor : Editor
{

    void OnSceneGUI()
    {
        //得到test脚本的对象
        SceneTest test = (SceneTest)target;

        //绘制文本框
        Handles.Label(test.transform.position + Vector3.up * 2,
                   test.transform.name + " : " + test.transform.position.ToString());

        //开始绘制GUI
        Handles.BeginGUI();

        //规定GUI显示区域
        GUILayout.BeginArea(new Rect(100, 100, 100, 100));

        //GUI绘制一个按钮
        if (GUILayout.Button("按钮!"))
        {
            Debug.Log("test");
        }
        //GUI绘制文本框
        GUILayout.Label("我在编辑Scene视图");

        GUILayout.EndArea();

        Handles.EndGUI();
    }

}
