using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BuffBaseInfo
{
    [Header("Buff的ID")]
    public int ID = 0;

    [Header("Buff的名称")]
    public string buffName = "";

    [Header("Buff的描述")]
    [TextArea(1,5)]
    public string description = "";

    [Header("Buff的图标")]
    public Sprite icon = null;

    [Header("最大叠加次数")]
    public int maxStack = 1;

    [Header("每次降级丢失的层数")]
    public int downStack = 1;

    [Header("是否显示")]
    public bool shouldShow = true;

    [Header("是否永久保持")]
    public bool keepForever = false;

    [Header("计时周期")]
    public float tickTime = 0.1f;
}

public class Buff : ScriptableObject
{
    [Header("配置基础信息")]
    [SerializeField]
    protected BuffBaseInfo info;
    protected float maxTime = 5;
    protected float keepTimer = 0;
    protected float tickTimer = 0;
    protected int stack = 1;

    public BuffBaseInfo Info => info;
    public float Percent => keepTimer / maxTime; //当前已经历的百分比

    public virtual int TipNum => 1; //将显示在buff栏下的小数字，可随不同的buff以不同逻辑显示(1默认不写)
    public virtual bool DevotionFlag => keepTimer > maxTime; //降级标记，标志什么时候应当被降级，默认为持有时间超出了buff时
    public virtual bool RemoveFlag => stack <= 0; //移除标记，标记什么时候应当被移除

    /// <summary>
    /// 应用Buff时的行为，默认添加一层
    /// </summary>
    /// <param name="time">buff持续时间，当time = -1时，设置该buff持续时间无限</param>
    public virtual void OnApply(float time)
    {
        if (time == -1)
            time = float.MaxValue;
        maxTime = time;

        stack = 1;
    }

    /// <summary>
    /// 重复添加Buff时的行为，默认为尝试添加一层并刷新持续时间
    /// </summary>
    public virtual void OnRepeat(float time)
    {
        if(time == -1)
            time = float.MaxValue;

        if (stack < info.maxStack) //尝试叠层
            stack++;

        if (maxTime - keepTimer > time) //若新加入的时间长度还不如原有的剩余时长，什么也不做
            return;

        maxTime = time;
        keepTimer = 0;
    }

    /// <summary>
    /// 降级行为，当达到降级标记时降级，默认降低一级，降低到0时移除
    /// </summary>
    public virtual void OnDevotion()
    {
        if (--stack >= 0)
            OnRemove();
    }

    /// <summary>
    /// 移除Buff时的行为
    /// </summary>
    public virtual void OnRemove()
    {

    }

    /// <summary>
    /// 更新Buff时的行为，默认更新计时器
    /// </summary>
    public virtual void OnUpdate()
    {
        float delta = Time.deltaTime;
        tickTimer += delta;
        if (tickTimer > info.tickTime)
        {
            tickTimer -= info.tickTime;
            OnTick();
        }
        if (!info.keepForever)
        {
            keepTimer += delta;
        }
    }

    /// <summary>
    /// 每个计时周期触发的行为
    /// </summary>
    protected virtual void OnTick()
    {
        // 在子类中实现具体的触发行为
    }
}
