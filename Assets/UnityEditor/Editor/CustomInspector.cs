/*==============================================================================
* 文件名:     CustomInspector 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 12:00:00 AM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/
using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;


[CustomEditor(typeof(UnityEditor.DefaultAsset))]

public class CustomInspector : Editor
{

    List<Data> datas = new List<Data>();

    public override void OnInspectorGUI()
    {
        Event e = Event.current;
        string path = AssetDatabase.GetAssetPath(target);

        GUI.enabled = true;
        //if (path.EndsWith (".unity")) 
        {
            Draw();
            if (e.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                e.Use();
            }
            if (e.type == EventType.DragPerform)
            {

                Object o1 = DragAndDrop.objectReferences[0];
                if (o1 is GameObject)
                {
                    datas.Add(new Data() { go = o1 as GameObject });
                }
            }
        }
    }

    Vector2 scrollPos = Vector2.zero;
    void Draw()
    {

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (Data data in datas)
        {
            var editor = Editor.CreateEditor(data.go);
            data.fold = EditorGUILayout.InspectorTitlebar(data.fold, data.go);


            if (data.fold)
            {
                editor.OnInspectorGUI();
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUILayout.BeginVertical();
                foreach (Component c in data.go.GetComponents<Component>())
                {
                    if (!data.editors.ContainsKey(c))
                        data.editors.Add(c, Editor.CreateEditor(c));
                }
                foreach (Component c in data.go.GetComponents<Component>())
                {
                    if (data.editors.ContainsKey(c))
                    {
                        data.foldouts[c] = EditorGUILayout.InspectorTitlebar(data.foldouts.ContainsKey(c) ? data.foldouts[c] : true, c);
                        if (data.foldouts[c])
                            data.editors[c].OnInspectorGUI();
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }

        }
        EditorGUILayout.EndScrollView();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    class Data
    {
        public GameObject go;
        public bool fold = true;
        public Dictionary<Object, Editor> editors = new Dictionary<Object, Editor>();
        public Dictionary<Object, bool> foldouts = new Dictionary<Object, bool>();
    }

}
