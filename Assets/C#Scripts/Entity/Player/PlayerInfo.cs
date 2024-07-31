using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo: EntityInfoBase
{
    #region 常量值
    private readonly static float minHealth = 1; //当最大生命值被降低时，可能达到的最小值 
    private readonly static float minMana = 1; //同上
    private readonly static float minSpeed = 1;
    private readonly static float minStrength = 1;
    #endregion

    #region 字段与属性
    [Header("标准属性")] //一些一般不会发生太多变动的属性
    [SerializeField] private float maxHealth;        // 最大生命
    [SerializeField] private float maxMana;          // 最大法力
    [SerializeField] private float maxExp;         // 经验条上限,下一次升级所需经验
    [SerializeField] private float speed;            // 移动速度
    [SerializeField] private float strength;         // 力量
    [SerializeField] private float accuracy;        // 精确度，代表每次打出时可能偏移的角度量
    [SerializeField] private float hitback;         //击退
    [SerializeField] private int level;            // 等级

    [Header("动态属性")] //一些在战斗中不断变化的属性
    [SerializeField] private float exp = 0;              // 经验值
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentMana;

    [Header("其它属性")] //一些其它的属性
    [SerializeField] private float manaPerSec;      // 每秒回复蓝量
    [SerializeField] private float healthPerSec;    // 每秒回复生命值
    [SerializeField] private float drawInterval;     // 抽牌间隔
    
    public Action<PlayerInfo> onInfoChanged;

    public void Init(PlayerInfoConfig config)
    {
        maxHealth = config.maxHealth;
        maxMana = config.maxMana;
        maxExp = config.maxExp;
        speed = config.speed;
        strength = config.strength;
        accuracy = config.accuracy;
        hitback = config.hitback;
        level = config.level;

        healthPerSec = config.healthPerSec;
        manaPerSec = config.manaPerSec;
        drawInterval = config.drawInterval;
    }
    

    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = Mathf.Max(minHealth, value);
            onInfoChanged?.Invoke(this);
        }
    }

    public float MaxMana
    {
        get { return maxMana; }
        set
        {
            maxMana = Mathf.Max(minMana, value);
            onInfoChanged?.Invoke(this);
        }
    }

    public float MaxExp
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
        }
    }

    public int Level
    {
        get { return level; }
        set
        {
            level = value;
        }
    }

    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            if(exp > maxExp)
            {
                exp -= maxExp;
                Level += 1;
                EventManager.Instance.onLevelUp?.Invoke(Level);
                //TODO: maxExp 将随之变化
            }
        }
    }

    public float Speed
    {
        get { return speed; }
        set
        {
            speed = Mathf.Max(minSpeed, value);
            onInfoChanged?.Invoke(this);
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            onInfoChanged?.Invoke(this);
        }
    }

    public float CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = Mathf.Clamp(value, 0, maxMana);
            onInfoChanged?.Invoke(this);
        }
    }

    public float Strength
    {
        get { return strength; }
        set
        {
            strength = Mathf.Max(minStrength, value);
            onInfoChanged?.Invoke(this);
        }
    }

    public float Accuracy
    {
        get { return accuracy; }
        set { accuracy = value; }
    }

    public float ManaPerSecond
    {
        get { return manaPerSec; }
        set { manaPerSec = value; }
    }

    public float HealthPerSecond
    {
        get { return healthPerSec; }
        set { healthPerSec = value; }
    }

    public float DrawInterval
    {
        get { return drawInterval; }
        set { drawInterval = value; }
    }

    public float Hitback
    {
        get { return hitback; }
        set
        {
            hitback = value;
            onInfoChanged?.Invoke(this);
        }
    }
    #endregion

    #region 函数
    public void Heal(float amount)
    {
        CurrentHealth += amount;
    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;
    }

    public void ChargeMana(float amount)
    {
        CurrentMana += amount;
    }

    public void ConsumeMana(float amount)
    {
        CurrentMana -= amount;
    }

    #endregion
}
