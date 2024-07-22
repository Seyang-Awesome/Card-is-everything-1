using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表现层的UI，与游戏逻辑分离，它只监听EventManager里的事件，和逻辑层CardManager无依赖关系
/// </summary>
public class CardUIPannel : MonoBehaviour
{
    [SerializeField]
    private Transform cardSlotsRoot;

    private CardSlot[] cardSlots;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        cardSlots = cardSlotsRoot.GetComponentsInChildren<CardSlot>();
        EventManager.Instance.onDraw += OnDraw;
        EventManager.Instance.onUse += OnUse;
        EventManager.Instance.onDiscard += OnDiscard;
    }

    private void OnDraw(Card card,int index)
    {
        cardSlots[index].Draw(card);
    }

    private void OnUse(Card card, int index)
    {
        cardSlots[index].Use();
    }

    private void OnDiscard(Card card, int index)
    {
        cardSlots[index].Discard();
    }
}
