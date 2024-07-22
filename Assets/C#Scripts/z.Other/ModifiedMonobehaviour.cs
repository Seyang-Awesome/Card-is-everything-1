using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔改过的Monobehaviour,省去Update的暂停判断
/// 简单来说，我喜欢用Update，但是有时候暂停的时候还会Update好麻烦，所以把Mono改成这个鬼样子了
/// </summary>
public class ModifiedMonobehaviour : MonoBehaviour
{
    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        PauseableUpdate();
    }

    /// <summary>
    /// 在游戏暂停时跟随暂停的Update喵
    /// </summary>
    protected virtual void PauseableUpdate()
    {

    }
}
