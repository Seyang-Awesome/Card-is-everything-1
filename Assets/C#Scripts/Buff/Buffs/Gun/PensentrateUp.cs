using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使子弹获得有限穿透，若要使其获得无限穿透，见Pensentrate
/// </summary>
[CreateAssetMenu(fileName = "PensentrateUp", menuName = "ScriptableObjects/Buff/PensentrateUp")]
public class PensentrateUp : Buff
{
    public override void OnApply(float durantion)
    {
        base.OnApply(durantion);

        EventManager.Instance.onBulletGenerate += Pensentrate;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        EventManager.Instance.onBulletGenerate -= Pensentrate;
    }

    private void Pensentrate(BulletInfo info)
    {
        info.ModifyPenetrate(stack);
    }

}
