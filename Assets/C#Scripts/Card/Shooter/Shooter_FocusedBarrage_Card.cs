using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FocusedBarrageCard", menuName = "ScriptableObjects/Card/FocusedBarrage")]
public class Shooter_FocusedBarrage_Card : Card
{
    [Header("卡牌特有属性")]
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private int loops = 6;

    public override void Use()
    {
        base.Use();
        DOTween.Sequence()
            .AppendInterval(interval)
            .SetLoops(loops)
            .OnStepComplete(Fire);
    }

    private void Fire()
    {
        var info = new BulletInfo(DataManager.Instance.prefabCenter.gunBulletDependency);
        info.ModifyRandomAngle(-30); //大幅度提高子弹精度
        info.ModifySpeedMultiplier(0.5f);
        GameLogicManager.Instance.Shoot(info);
    }
}
