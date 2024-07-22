using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class CardInfo
{
    public Action<CardInfo> onInfoChanged;

    public Sprite pattern; //卡面图案
    public int baseCostMana; //基础法力消耗(即不考虑任何修正前的最基本消耗)
    public string cardName;  //卡名
    [TextArea(1,5)]
    public string description; //卡牌描述

    private int costMana = -1; //当前法力消耗，在未初始化时为-1，会在首次尝试获取CostMana时将法力消耗设置为baseCostMana
    public int CostMana
    {
        get 
        {
            if (costMana == -1)
                costMana = baseCostMana;
            return costMana; 
        }
        set 
        {
            if (costMana == -1)
                costMana = baseCostMana;
            costMana = Mathf.Max(value, 0); 
            onInfoChanged?.Invoke(this);
        }
    }
}

public class Card : ScriptableObject
{
    [SerializeField]
    private CardInfo info;
    public CardInfo Info => info;

    protected int handIndex = -1; //在手牌中的位置

    public virtual void Draw(int index) { handIndex = index;  }  //被抽取时调用
    public virtual void Use() {  } //被打出时效果
    public virtual void Discard() { } //被弃置时效果，弃置暂时未实现  
    public virtual bool IsCanUse() { return Player.Instance.Info.CurrentMana >= info.CostMana; } //卡牌可以打出的默认条件: 当前玩家法力大于该卡牌所需的法力

    public virtual void Cost() { Player.Instance.StatManager.ConsumeMana(info.CostMana); } //消耗法力
}
