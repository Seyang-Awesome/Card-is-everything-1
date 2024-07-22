using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllArmCard", menuName = "ScriptableObjects/Card/AllArm")]
public class Shooter_AllArm_Card : Card
{
    public override void Use()
    {
        base.Use();

        for(int i = 0; i < Consts.playerHandCardCount; i++) 
        {
            CardManager.Instance.TryDraw();
        }
    }
}
