using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    private Dictionary<Type,Buff> buffDict = new(); //buff池，初始化时存储进所有buff

    private Dictionary<int, Buff> currentBuffs = new(); //玩家持有的buff效果

    protected override void Awake()
    {
        base.Awake();
        foreach(var b in DataManager.Instance.buffCenter.buffs)
        {
            buffDict.Add(b.GetType(), b);
        }
    }

    /// <summary>
    /// 应用一个给定时长的BUFF
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="durantion"></param>
    public void ApplyBuff<T>(float durantion) where T : Buff,new()
    {
        var buff = buffDict[typeof(T)];
        if (buff == null)
        {
            Debug.LogError("错误，类型为" + typeof(T) + "的BUFF不存在");
            return;
        }

        var id = buff.Info.ID;
        if (currentBuffs.ContainsKey(id))
        {
            // BuffDict中已存在相同类型的Buff，进行相应逻辑处理
            Buff existingBuff = currentBuffs[id];
            existingBuff.OnRepeat(durantion);
        }
        else
        {
            // BuffDict中不存在相同类型的Buff，添加新的Buff
            T newBuff = Instantiate(buff) as T;
            currentBuffs.Add(id, newBuff);
            newBuff.OnApply(durantion);
        }
    }

    protected override void PauseableUpdate()
    {
        int[] ID = currentBuffs.Keys.ToArray();
        foreach(var id in ID)
        {
            var b = currentBuffs[id];
            b.OnUpdate();

            if (b.DevotionFlag)
                b.OnDevotion();
            if (b.RemoveFlag)
                currentBuffs.Remove(b.Info.ID);
        }
    }
}
