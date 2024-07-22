using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReloadingCard", menuName = "ScriptableObjects/Card/ReloadingFire")]
public class Shooter_Reloading_Card : Card
{

    public override void Use()
    {
        base.Use();
        for(int i = 0; i < Consts.playerHandCardCount; i++) 
        {
            CardManager.Instance.TryDiscard(i);

            Player.Instance.StatManager.ChargeMana(Player.Instance.Info.MaxMana);
        }
    }
}
