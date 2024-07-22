using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform panelRoot; // 对应的Canvas
    [SerializeField] private Image[] panelLayers; // UI面板的层级
    [SerializeField] private bool isCanOperateUI; // 是否可以进行UI操作
    [SerializeField] private Image uiMask; // UI的遮罩，当这个东西启用时无法进行任何UI操作
    [SerializeField] private PanelContainer panelContainer; // 各种UI面板预制体的引用

    private Dictionary<Type, PanelBase> panelDic = new(); // 存有的UI面板预制体的引用
    private Dictionary<Type, PanelBase> panelOnShowing = new(); // 正在显示中的UI面板
    private Dictionary<int, bool> isHavePanelShowLayer = new(); // 对应层级是否有UI面板正在显示

    private void Start()
    {
        // 初始化面板字典
        panelContainer.panels.ForEach(panel => panelDic.Add(panel.GetType(), panel));

        // 初始化层级字典
        isHavePanelShowLayer = new()
        {
            {0, false},
            {1, false},
            {2, false},
            {3, false},
            {4, false},
        };

        // 初始化UI操作和遮罩
        isCanOperateUI = true;
        uiMask.enabled = false;
    }

    // 显示指定类型的面板，并返回面板实例
    public T ShowPanel<T>() where T : PanelBase
    {
        return ShowPanel(typeof(T)) as T;
    }

    // 显示指定类型的面板，并返回面板实例
    public PanelBase ShowPanel(Type panelType)
    {
        // 如果无法进行UI操作、面板类型不是PanelBase的子类或者面板字典中不存在该类型的面板，则返回null
        if (!isCanOperateUI) 
            return null;

        if (!typeof(PanelBase).IsAssignableFrom(panelType) || !panelDic.ContainsKey(panelType)) 
            return null;

        // 如果该类型的面板正在显示中，则调用面板的OnShowingAndCall方法并返回面板实例
        if (panelOnShowing.ContainsKey(panelType))
        {
            panelOnShowing[panelType].OnShowingAndCall();
            return panelOnShowing[panelType];
        }

        PanelBase relevantPanel = panelDic[panelType]; // 获取相关的面板
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer; // 获取面板的层级

        // 如果该层级已经有面板正在显示，则返回null
        if (isHavePanelShowLayer[relevantPanel.panelSortingLayer]) 
            return null;

        PanelBase panel = PoolManager.Instance.GetGameObject(relevantPanel, panelLayers[relevantPanelSortingLayer].transform); // 从对象池中获取面板实例
        panel.OnShow(); // 调用面板的OnShow方法

        panelOnShowing.Add(panelType, panel); // 将面板添加到正在显示中的字典中
        isHavePanelShowLayer[relevantPanelSortingLayer] = true; // 设置该层级有面板正在显示
        panelLayers[relevantPanelSortingLayer].raycastTarget = true; // 开启该层级的射线检测

        return panel;
    }

    // 隐藏指定类型的面板,泛型重载
    public void HidePanel<T>() where T : PanelBase
    {
        HidePanel(typeof(T));
    }

    public void HidePanel(Type panelType)
    {
        // 如果无法进行UI操作、面板字典中不存在该类型的面板或者面板类型不是PanelBase的子类，则返回
        if (!isCanOperateUI)
            return;

        if (!panelOnShowing.ContainsKey(panelType) || !typeof(PanelBase).IsAssignableFrom(panelType))
            return;

        // 如果该类型的面板正在隐藏中，则调用面板的OnHiding方法并返回
        if (panelOnShowing[panelType].isHiding)
        {
            panelOnShowing[panelType].OnHiding();
            return;
        }

        PanelBase relevantPanel = panelOnShowing[panelType]; // 获取相关的面板
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer; // 获取面板的层级

        relevantPanel.OnHide(); // 调用面板的OnHide方法

        if (relevantPanel.isHideDirectly)
        {
            PoolManager.Instance.PushGameObject(relevantPanel.gameObject); // 将面板实例放回对象池
            panelOnShowing.Remove(panelType); // 从正在显示中的字典中移除面板
            isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false; // 设置该层级没有面板正在显示
            panelLayers[relevantPanelSortingLayer].raycastTarget = false; // 关闭该层级的射线检测
        }
    }

    public void ClearPanelCache<T>() where T : PanelBase
    {
        ClearPanelCache(typeof(T));
    }

    public void ClearPanelCache(Type panelType)
    {
        // 如果无法进行UI操作、面板字典中不存在该类型的面板或者面板类型不是PanelBase的子类，则返回
        if (!isCanOperateUI)
            return;

        if (!panelOnShowing.ContainsKey(panelType) || !typeof(PanelBase).IsAssignableFrom(panelType))
            return;

        Debug.Log("ClearPanelCache");

        PanelBase relevantPanel = panelOnShowing[panelType]; // 获取相关的面板
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer; // 获取面板的层级

        PoolManager.Instance.PushGameObject(relevantPanel.gameObject); // 将面板实例放回对象池
        panelOnShowing.Remove(panelType); // 从正在显示中的字典中移除面板
        isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false; // 设置该层级没有面板正在显示
        panelLayers[relevantPanelSortingLayer].raycastTarget = false; // 关闭该层级的射线检测
    }

    public T GetPanelOnShowing<T>() where T : PanelBase
    {
        return panelOnShowing.ContainsKey(typeof(T)) ? panelOnShowing[typeof(T)] as T : null;
    }

    public void LoadSceneAsync(string sceneName,Action onLoadCompeleted = null)
    {
        SetMask(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                break;
        }

        operation.allowSceneActivation = true;
        // await UniTask.DelayFrame(5);
        onLoadCompeleted?.Invoke();

        SetMask(false);
    }

    public void SetMask(bool b)
    {
        isCanOperateUI = b;
        uiMask.enabled = b;
    }
}