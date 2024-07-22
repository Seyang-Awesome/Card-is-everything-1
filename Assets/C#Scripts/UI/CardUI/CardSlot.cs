using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private CardFace cardFace;

    /// <summary>
    /// 使用本卡槽的卡牌
    /// </summary>
    public void Use()
    {
        cardFace.OnUse();
    }

    /// <summary>
    /// 向此卡槽抽取卡牌时
    /// </summary>
    /// <param name="card">被抽取的卡牌</param>
    public void Draw(Card card) 
    {
        cardFace = Instantiate(DataManager.Instance.prefabCenter.cardFace, transform.position, Quaternion.identity,transform);
        cardFace.Init(card);
        cardFace.OnShow();
    }

    /// <summary>
    /// 从该卡槽弃牌时
    /// </summary>

    public void Discard() 
    {
        cardFace.OnDiscard();
    }
}
