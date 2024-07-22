using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使子弹获得无限穿透，若要使其获得有限的几次穿透，见PensentrateUp
/// </summary>
[CreateAssetMenu(fileName = "Pensentrate", menuName = "ScriptableObjects/Buff/Pensentrate")]
public class Pensentrate : Buff
{
    public override void OnApply(float durantion)
    {
        base.OnApply(durantion);

        EventManager.Instance.onBulletGenerate += PensentrateBullet;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        EventManager.Instance.onBulletGenerate -= PensentrateBullet;
    }

    private void PensentrateBullet(BulletInfo info)
    {
        info.AddAbility(BulletAbility.Penetrate);
    }
}
