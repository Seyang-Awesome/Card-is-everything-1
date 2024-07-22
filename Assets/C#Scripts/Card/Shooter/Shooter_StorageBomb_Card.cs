using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//蓄爆卡牌
[CreateAssetMenu(fileName ="StorageBombCard",menuName = "ScriptableObjects/Card/StorageBomb")]
public class Shooter_StorageBomb_Card : Card
{
    [Header("卡牌特有属性")]
    [SerializeField] private float waitTime = 1;
    [SerializeField] private int shootCount = 8;
    [SerializeField] private float maxOffsetAngle = 15;
    [SerializeField] private float maxSpeedModifier = 1;
    [SerializeField] private float cutLifeTime = 0.9f;

    public override void Use()
    {
        base.Use();
        ScheduleManager.Instance.AddSchedule(new Schedule(waitTime,Fire));  //1s后随后打出大量弹药
    }

    private void Fire()
    {
        //发射数枚增加了偏转，速度加快但存在时间低的子弹
        for (int i = 0; i < shootCount; i++)
        {
            var info = new BulletInfo(DataManager.Instance.prefabCenter.gunBullet);
            info.ModifyRandomAngle(maxOffsetAngle);
            info.ModifySpeedMultiplier(Random.Range(0,maxSpeedModifier));
            info.ModifyLifeTimeMultiplier(-cutLifeTime);
            GameLogicManager.Instance.Shoot(info);
        }
    }
}
