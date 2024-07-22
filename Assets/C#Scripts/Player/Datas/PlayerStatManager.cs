using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来处理一些玩家信息中的值
/// 简单来说不想让info又存储数据又运行函数，所以把这些回蓝回血的函数单独搬一个出来
/// </summary>
public class PlayerStatManager
{
    private PlayerInfo info;

    private float infiniteTimer;

    public void Init(Player player)
    {
        info = player.Info;
    }

    public void OnUpdate()
    {
        info.CurrentHealth += info.HealthPerSecond * Time.deltaTime;
        info.CurrentMana += info.ManaPerSecond * Time.deltaTime;

        infiniteTimer -= Time.deltaTime;
    }

    //下面这些函数可以加事件
    public void TakeHit(float damage)
    {
        if (infiniteTimer > 0)
            return;

        infiniteTimer = Consts.infiniteTime;
        info.Damage(damage);
    }

    public void GetHeal(float amount)
    {
        info.Heal(amount);
    }

    public void ChargeMana(float amount)
    {
        info.ChargeMana(amount);
    }

    public void ConsumeMana(float amount)
    {
        info.ConsumeMana(amount);
    }
}
