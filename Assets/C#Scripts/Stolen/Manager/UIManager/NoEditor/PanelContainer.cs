using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Container/PanelContainer", fileName = "PanelContainer")]
public class PanelContainer : ScriptableObject
{
    public List<PanelBase> panels = new();

    //外置添加面板函数，若没有此类别的pannel则加入，已有此类别的pannel则更新
    public void AddPanel(PanelBase panel)
    {
        int sameTypePanelIndex = IsContainSameTypePanel(panel);
        if (sameTypePanelIndex == -1)
            panels.Add(panel);
        else
            panels[sameTypePanelIndex] = panel;
    }

    public int IsContainSameTypePanel(PanelBase panel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i] == null)
            {
                panels.Remove(panels[i]);
                i--;
            }
        }
        foreach (var item in panels)
        {
            if (item.GetType() == panel.GetType())
                return panels.IndexOf(item);
        }

        return -1;
    }
}

