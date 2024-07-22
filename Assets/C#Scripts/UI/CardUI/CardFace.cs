using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏卡面
/// </summary>
public class CardFace : MonoBehaviour
{
    private Card card;

    public Image cardPattern;
    public Text costManaText;
    public Text cardNameText;
    public Text cardDescriptionText;

    /// <summary>
    /// 初始化卡面
    /// </summary>
    /// <param name="card"></param>
    public void Init(Card card)
    {
        this.card = card;
        UpdateInfo(card.Info);
        card.Info.onInfoChanged += UpdateInfo;
    }

    private void UpdateInfo(CardInfo info)
    {
        cardPattern.sprite = info.pattern;
        costManaText.text = info.CostMana.ToString();
        cardNameText.text = info.cardName;
        cardDescriptionText.text = info.description;
    }

    /// <summary>
    /// 当卡牌被抽取展示时
    /// </summary>
    public void OnShow()
    {
        var pos = transform.position;
        transform.position += Vector3.down * 200;
        transform.DOMove(pos, 1f).SetEase(Ease.InOutQuad);
    }

    /// <summary>
    /// 当卡牌被使用时
    /// </summary>
    public void OnUse()
    {
        var p = DataManager.Instance.prefabCenter.cardParticle;
        EffectManager.Instance.GenerateParticle(p, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    /// <summary>
    /// 当卡牌被丢弃时
    /// </summary>
    public void OnDiscard()
    {
        var p = DataManager.Instance.prefabCenter.cardParticle;
        EffectManager.Instance.GenerateParticle(p, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    /// <summary>
    /// 卡牌摧毁时，注销事件
    /// </summary>
    private void OnDestroy()
    {
        card.Info.onInfoChanged -= UpdateInfo;
    }
}
