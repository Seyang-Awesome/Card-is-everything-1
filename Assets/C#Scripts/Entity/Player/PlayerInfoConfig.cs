using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerInfoConfig", fileName = "PlayerInfoConfig")]
public class PlayerInfoConfig : ScriptableObject
{
    [Header("标准属性")] //一些一般不会发生太多变动的属性
    public float maxHealth = 10;          // 最大生命
    public float maxMana = 10;            // 最大法力
    public float maxExp = 100;            // 经验条上限,下一次升级所需经验
    public float speed = 5;               // 移动速度
    public float strength = 5;            // 力量
    public float accuracy = 10;           // 精确度，代表每次打出时可能偏移的角度量
    public float hitback = 1;             //击退
    public int level = 1;                 // 等级

    [Header("其它属性")] //一些其它的属性
    public float manaPerSec = 1f;         // 每秒回复蓝量
    public float healthPerSec = 0f;       // 每秒回复生命值
    public float drawInterval = 2f;       // 抽牌间隔
}

