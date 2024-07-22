using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConnectingFireCard", menuName = "ScriptableObjects/Card/ConnectingFire")]
public class Shooter_ConnectingFire_Card : Card
{
    public override void Use()
    {
        base.Use();

        CardManager.Instance.TryUse(handIndex - 1, true);
        CardManager.Instance.TryUse(handIndex + 1, true);
    }

}
