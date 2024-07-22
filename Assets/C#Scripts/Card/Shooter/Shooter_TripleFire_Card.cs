using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TripleFireCard", menuName = "ScriptableObjects/Card/TripleFire")]
public class Shooter_TripleFire_Card : Card
{
    [Header("卡牌特有属性")]
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private int loops = 3;

    public override void Use()
    {
        base.Use();
        DOTween.Sequence()
            .AppendInterval(interval)
            .SetLoops(loops)
            .OnStepComplete(Fire);
        BuffManager.Instance.ApplyBuff<StopShooting>(loops * interval); //射击的同时阻塞玩家基础射击
    }

    private void Fire()
    {
        var info = new BulletInfo(DataManager.Instance.prefabCenter.gunBullet);
        GameLogicManager.Instance.Shoot(info);
    }
}
