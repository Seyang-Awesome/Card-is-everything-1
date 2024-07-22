using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperBulletCard", menuName = "ScriptableObjects/Card/SuperBulletFire")]
public class Shooter_SuperBullet_Card : Card
{
    [Header("卡牌特有属性")]
    [SerializeField] private int stacks = 3; //可使用次数
    [SerializeField] private int keepTime = 20; //持续时间

    public override void Use()
    {
        base.Use();
        for(int i = 0; i < stacks; i ++)
            BuffManager.Instance.ApplyBuff<ScaleUp>(keepTime);
    }
}
