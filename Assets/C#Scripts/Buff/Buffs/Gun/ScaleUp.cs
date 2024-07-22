using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 测试buff，放大子弹
/// </summary>
[CreateAssetMenu(fileName = "ScaleUp",menuName ="ScriptableObjects/Buff/ScaleUp")]
public class ScaleUp : Buff
{
    public override void OnApply(float durantion)
    {
        base.OnApply(durantion);

        EventManager.Instance.onUse += OnUse;
        EventManager.Instance.onBulletGenerate += BulletScaleUp;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        EventManager.Instance.onUse -= OnUse;
        EventManager.Instance.onBulletGenerate -= BulletScaleUp;
    }

    private void BulletScaleUp(BulletInfo info)
    {
        info.ModifyScale(1f);
        info.ModifyDamageMultiplier(0.5f);
    }

    private void OnUse(Card c, int i)
    {
        OnDevotion();
    }
}
