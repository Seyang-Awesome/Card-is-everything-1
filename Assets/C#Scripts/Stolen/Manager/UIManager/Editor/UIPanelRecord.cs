using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIPanelRecord
{
    private const string panelContainerPath = @"Assets/C#Script/Stolen/Manager/UIManager/PanelContainer.asset";
    private static PanelContainer _panelContainer;

    // 面板容器的属性，用于获取面板容器的实例
    private static PanelContainer panelContainer
    {
        get
        {
            if (_panelContainer == null)
            {
                // 如果面板容器实例为空，则尝试从指定路径加载面板容器
                _panelContainer = AssetDatabase.LoadAssetAtPath<PanelContainer>(panelContainerPath);
                if (_panelContainer == null)
                {
                    // 如果面板容器文件不存在，则创建一个新的面板容器实例，并将其保存到指定路径
                    _panelContainer = ScriptableObject.CreateInstance<PanelContainer>();
                    AssetDatabase.CreateAsset(_panelContainer, panelContainerPath);
                    EditorUtility.SetDirty(_panelContainer);
                    AssetDatabase.SaveAssets();
                }
            }

            return _panelContainer;
        }
    }


    // 菜单项 "Assets/SavePanel" 的回调方法
    [MenuItem("Assets/SavePanel")]
    static void Select()
    {
        string[] guids = Selection.assetGUIDs;
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            GameObject panelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (panelPrefab == null)
                return;

            PanelBase panelComponent = panelPrefab.GetComponent<PanelBase>();

            if (panelComponent == null)
                return;

            // 将面板组件添加到面板容器中
            panelContainer.AddPanel(panelComponent);
            EditorUtility.SetDirty(panelContainer);
            AssetDatabase.SaveAssets();

        }
    }

    // 菜单项 "Assets/SavePanel" 的验证方法，用于判断是否可以执行菜单项的回调方法
    [MenuItem("Assets/SavePanel", true)]
    static bool IsSelect()
    {
        // 只有当选中的资源不为空且至少有一个资源被选中时，才返回 true，表示可以执行菜单项的回调方法
        return null != Selection.assetGUIDs && Selection.assetGUIDs.Length > 0;
    }
}