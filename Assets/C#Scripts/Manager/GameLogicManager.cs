using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一些全局的行为管理器
/// </summary>
public class GameLogicManager : Singleton<GameLogicManager>
{
    /// <summary>
    /// 子弹生成时调用，公布事件并让所有监听者修改生成的子弹信息
    /// </summary>
    /// <param name="info">待修改的子弹信息</param>
    public BulletInfo ModifyBulletInfo(BulletInfo info)
    {
        EventManager.Instance.onBulletGenerate?.Invoke(info);
        return info;
    }

    /// <summary>
    /// 发射一颗子弹时调用，提供准备好的信息并在此处发射子弹
    /// </summary>
    /// <param name="info">子弹信息</param>
    public void Shoot(BulletInfo info)
    {
        ModifyBulletInfo(info); //发送事件修正信息
        var b = PoolManager.Instance.GetGameObject(info.BulletDependency);
        b.Launch(info);
    }

    /// <summary>
    /// 无参重载，提供一个默认的子弹信息类
    /// </summary>
    public void Shoot()
    {
        var info = new BulletInfo(DataManager.Instance.prefabCenter.gunBulletDependency);
        Shoot(info);
    }

    /// <summary>
    /// 子弹命中敌人时调用，提供子弹信息和敌人信息并交由各监听者处理结果
    /// </summary>
    /// <param name="info"></param>
    public void OnBulletHitEnemy(BulletInfo info)
    {

    }
}
