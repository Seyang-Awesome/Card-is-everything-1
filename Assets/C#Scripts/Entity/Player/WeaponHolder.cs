using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 持有武器的类，用于调整玩家的攻击朝向
/// 类中大量的被注释掉的代码作用是: 最初的想法里想让玩家在不打出卡牌时也能自动隔一段时间普攻一次，但测试下来效果不佳
/// 因此将自动普攻的效果注释掉了，只允许玩家通过打出卡牌来发射子弹。
/// 如果后续需要修改，可以考虑还原代码
/// </summary>
public class WeaponHolder : ModifiedMonobehaviour
{
    private PlayerInfo info;
    private Transform playerTransform;

    //private float lastShootTime = 0; //记录上次射击的时间
    //private int stuckCounter = 0; //阻塞射击行为的计数器，每当一个阻塞信息输入时，计数器加一，借此判断当前是否有阻塞
    //private bool IsInterval => Time.time - lastShootTime < player.Info.Interval; //判定攻击是否在冷却
    //private bool CanShoot => stuckCounter <= 0; //判定攻击是否被阻塞

    public void Init(PlayerInfo info, Transform playerTransform)
    {
        this.info = info;
        this.playerTransform = playerTransform;

        //正常打出的子弹，默认添加上一下三个修正
        //1.为其添加等同于玩家攻击力的伤害 2.将其基准位置设置为武器位置 3.为其添加等同于玩家准度的随机偏移值
        EventManager.Instance.onBulletGenerate += (info) =>
        {
            info.ModifyDamage(this.info.Strength);
            info.SetBase(transform.position, transform.rotation);
            info.ModifyRandomAngle(this.info.Accuracy);
            info.ModifyHitback(this.info.Hitback);
        };

        /*
        //注册事件用于外界阻塞射击功能
        EventManager.Instance.stuckShooting += () => stuckCounter++;
        EventManager.Instance.applyShooting += () =>
        {
            stuckCounter--;
            lastShootTime = Time.time;
        };
        */
    }

    protected override void PauseableUpdate()
    {
        UpdateTarget();
    }


    /// <summary>
    /// 更新朝向
    /// </summary>
    private void UpdateTarget()
    {
        Vector2 mouse = MyExtensions.MousePosition;
        Vector2 dir = mouse - (Vector2)playerTransform.position;
        transform.right = dir;
    }

    /*
    /// <summary>
    /// 若可射击，尝试射击
    /// </summary>
    private void TryShoot()
    {
        if (IsInterval || !CanShoot) //在攻击冷却或被其它组件阻塞射击时，返回
            return;

        var info = new BulletInfo(DataManager.Instance.prefabCenter.gunBullet); //创建一份子弹信息
        Shoot(info); //以该子弹信息打出子弹
    }

    public void Shoot(BulletInfo info)
    {
        lastShootTime = Time.time;
        GameLogicManager.Instance.Shoot(info);
    }
    */
}
