using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    #region 字段
    private List<Card> cardSet = new(); //卡组
    private List<Card> drawHeap = new(); //抽牌堆
    private List<Card> discardHeap = new(); //弃牌堆
    private Card[] handCards = new Card[Consts.playerHandCardCount]; //玩家手牌
    private float drawTimer = 0;

    private CardSlot[] slots; //玩家的四个卡槽
    #endregion

    #region 属性
    private bool HasEmptySlots => CheckEmptySlots(); //玩家是否有空卡槽
    #endregion

    #region 内部函数
    protected override void Awake()
    {
        base.Awake();
        slots = GetComponentsInChildren<CardSlot>();
    }
    protected override void PauseableUpdate()
    {
        DrawTimer(); //应用抽卡计时器
    }

    /// <summary>
    /// 检查空槽
    /// </summary>
    /// <returns>是否有空槽位</returns>
    private bool CheckEmptySlots()
    {
        bool result = false;
        foreach (var c in handCards) 
            result |= c == null;
        return result;
    }

    /// <summary>
    /// 自动抽卡计时器，逐帧更新
    /// </summary>
    private void DrawTimer()
    {
        if (!HasEmptySlots) //当没有空卡槽时，计时器不生效
        {
            drawTimer = 0;
            return;
        }

        drawTimer += Time.deltaTime;
        if(drawTimer > Player.Instance.Info.DrawInterval) //有空卡槽时，计时器计时，每隔固定间隔抽取一张卡
        {
            drawTimer -= Player.Instance.Info.DrawInterval;
            Draw();
        }
    }

    /// <summary>
    /// 抽牌，当卡牌不足时洗牌，有卡牌时抽取一张并置入第一个没有空位的卡槽
    /// </summary>
    private void Draw()
    {
        if (drawHeap.Count == 0) //若抽牌堆无卡牌，洗牌
            Shuffle();
        if (drawHeap.Count == 0) //偶牛批，一张牌都没了
            return;

        for(int i = 0; i < handCards.Length; i++) 
        {
            if (handCards[i] == null)
            {
                handCards[i] = drawHeap[0];
                EventManager.Instance.onDraw?.Invoke(drawHeap[0], i);
                drawHeap.RemoveAt(0);
                handCards[i].Draw(i);
                return;
            }
        }
    }

    /// <summary>
    /// 使用第index个槽位的卡牌
    /// </summary>
    /// <param name="index">槽位序号</param>
    /// <param name="forceUse">强制使用，指以某些手段直接跳过花费法力使用</param>
    /// <returns></returns>
    private bool Use(int index, bool forceUse)
    {
        var card = handCards[index];
        if (card == null || (!card.IsCanUse() && !forceUse)) //检查该位置手牌是否存在，检查其是否可使用
            return false;

        if (!forceUse)
            card.Cost();

        EventManager.Instance.onUse?.Invoke(card, index); //唤起使用事件
        handCards[index] = null;

        card.Use();

        discardHeap.Add(card); //置入弃牌堆
        return true;
    }

    /// <summary>
    /// 弃置第index个槽位的卡牌
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool Discard(int index)
    {
        var card = handCards[index];
        if (card == null) //检查该位置手牌是否存在，检查其是否可使用
            return false;

        card.Discard();

        EventManager.Instance.onDiscard?.Invoke(card, index);
        handCards[index] = null;
        discardHeap.Add(card); //置入弃牌堆
        return true;
    }

    /// <summary>
    /// 卡组抽尽后调用，将弃牌堆中的卡牌洗牌重新加入抽牌堆
    /// </summary>
    private void Shuffle() 
    {
        drawHeap = discardHeap.Shuffled(); 
        discardHeap.Clear();
        EventManager.Instance.onShuffle?.Invoke();
    }

    #endregion

    #region 外部接口
    public bool TryUse(int index, bool forceUse = false)
    {
        if(index < 0 || index > Consts.playerHandCardCount)
            return false;

        return Use(index, forceUse);
    }
    public bool TryDiscard(int index)
    {
        if (index < 0 || index > Consts.playerHandCardCount)
            return false;

        return Discard(index);
    }
    public void TryDraw()
    {
        Draw();
    }
    public void AddCardToDrawHeap(Card card)
    {
        drawHeap.Add(card);
        drawHeap.Shuffled();
    }
    public void AddCardToDiscardHeap(Card card)
    {
        discardHeap.Add(card);
        discardHeap.Shuffled();
    }
    #endregion

    #region debug
    public List<Card> testCards;
    private void Start()
    {
        cardSet = new();
        foreach(var c in testCards)
        {
            cardSet.Add(Instantiate(c));
        }
        drawHeap = new(cardSet);
    }
    #endregion
}

