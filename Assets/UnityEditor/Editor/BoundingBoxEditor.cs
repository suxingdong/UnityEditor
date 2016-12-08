/*==============================================================================
* 文件名:     BoundingBoxEditor 
* 作者：      East.Su
* 公司:       龙语游戏
* 创建时间:   12/8/2016 3:01:34 PM
* 版本号:     1.0.0
* 描述: 
*
* ==============================================================================
*/



using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

public class ToolsEditor : Editor
{
    [MenuItem("Tools/BoundingBox")]
    static void AutoMakeBox()
    {
        Transform parent = Selection.activeGameObject.transform;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider child in colliders)
        {
            DestroyImmediate(child);
        }
        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Transform>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;
    }

    [MenuItem("Tools/FindCenter")]
    static void FindCenter()
    {
        Transform parent = Selection.activeGameObject.transform;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;


        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Transform>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

        foreach (Transform t in parent)
        {
            t.position = t.position - bounds.center;
        }
        parent.transform.position = bounds.center + parent.position;

    }


    public class ColorData
    {
        public string name;
        public Color color;
    }


    [MenuItem("Tools/Creat Color")]
    static void Build()
    {
        //复制一份新的模板到newColorPath目录下
        string templateColorPath = "Assets/Template/color.colors";
        string newColorPath = "Assets/Editor/界面1.colors";
        AssetDatabase.DeleteAsset(newColorPath);
        AssetDatabase.CopyAsset(templateColorPath, newColorPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


        //这里我写了两条临时测试数据
        List<ColorData> colorList = new List<ColorData>(){
            new ColorData(){name ="红",color = Color.green},
            new ColorData(){name ="绿", color =new Color(0.1f,0.1f,0.1f,0.1f)}

        };

        UnityEngine.Object newColor = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(newColorPath);
        SerializedObject serializedObject = new SerializedObject(newColor);
        SerializedProperty property = serializedObject.FindProperty("m_Presets");
        property.ClearArray();

        //把我的测试数据写进去
        for (int i = 0; i < colorList.Count; i++)
        {
            property.InsertArrayElementAtIndex(i);
            SerializedProperty colorsProperty = property.GetArrayElementAtIndex(i);
            colorsProperty.FindPropertyRelative("m_Name").stringValue = colorList[i].name;
            colorsProperty.FindPropertyRelative("m_Color").colorValue = colorList[i].color;
        }
        //保存
        serializedObject.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    
    [MenuItem("Tools/Cleanup Missing Scripts")]
    static void CleanupMissingScripts()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            var gameObject = Selection.gameObjects[i];

            // We must use the GetComponents array to actually detect missing components
            var components = gameObject.GetComponents<Component>();

            // Create a serialized object so that we can edit the component list
            var serializedObject = new SerializedObject(gameObject);
            // Find the component list property
            var prop = serializedObject.FindProperty("m_Component");

            // Track how many components we've removed
            int r = 0;

            // Iterate over all components
            for (int j = 0; j < components.Length; j++)
            {
                // Check if the ref is null
                if (components[j] == null)
                {
                    // If so, remove from the serialized component array
                    prop.DeleteArrayElementAtIndex(j - r);
                    // Increment removed count
                    r++;
                }
            }

            // Apply our changes to the game object
            serializedObject.ApplyModifiedProperties();
            //这一行一定要加！！！
            EditorUtility.SetDirty(gameObject);
        }
    }


    [MenuItem("Tools/查看磁盘占用")]
    public static void menu()
    {
        Texture target = Selection.activeObject as Texture;
        var type = Types.GetType("UnityEditor.TextureUtil", "UnityEditor.dll");
        MethodInfo methodInfo = type.GetMethod("GetStorageMemorySize", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

        Debug.Log("内存占用：" + EditorUtility.FormatBytes(Profiler.GetRuntimeMemorySize(Selection.activeObject)));
        Debug.Log("硬盘占用：" + EditorUtility.FormatBytes((int)methodInfo.Invoke(null, new object[] { target })));
    }
}




