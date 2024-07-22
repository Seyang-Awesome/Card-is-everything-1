using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (隐式)阻塞射击buff
/// </summary>
[CreateAssetMenu(fileName = "StopShooting", menuName = "ScriptableObjects/Buff/StopShooting")]
public class StopShooting :Buff
{
    public override void OnApply(float durantion)
    {
        base.OnApply(durantion);

        EventManager.Instance.stuckShooting?.Invoke();
    }

    public override void OnRemove()
    {
        base.OnRemove();

        EventManager.Instance.applyShooting?.Invoke();
    }
}
