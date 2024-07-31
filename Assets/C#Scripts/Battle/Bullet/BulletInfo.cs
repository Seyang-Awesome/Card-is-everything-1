using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

/// <summary>
/// 子弹的额外能力，当然了，下面的枚举只是参考，我一个也没实现~
/// </summary>
public enum BulletAbility
{
    Chasing = 1, //跟踪
    Penetrate = 2, //穿透
    Froze = 4 //冰冻
}

public class BulletInfo
{
    #region 数据字段

    private int ability;    //武器具有的能力
    private float damage = 1; //子弹伤害
    private float damageMultiplier = 1; //伤害倍率
    private float speed = 30; //子弹速度
    private float speedMultiplier = 1;  //速度倍率
    private float lifeTime = 1.5f;
    private float lifeTimeMultiplier = 1;
    
    private Vector2 basePosition;  //基准位置，即玩家当前所在位置，这个字段只由玩家调控
    private Quaternion baseRotation;  //基准角度，即玩家所指向的角度，也只由玩家调控
    private Vector2 offsetPosition = Vector2.zero; //偏移位置，即相对原始位置的偏移值
    private Quaternion offsetRotation = Quaternion.identity; //偏移角度，即相对原始角度的偏移值，这部分是用于阵型式的射击时使用的，随机偏移角使用randomAngle
    
    private float randomAngle = 0; //随机偏移角
    private float scale = 1;
    private float hitback = 1f;
    private int penetrate = 0; //穿透数
    
    public int TargetLayer { get; }
    public IEntity Source;
    
    #endregion
    
    #region 子弹逻辑运作阶段调用的数值
    public float Damage => Mathf.Max(1, damage) * Mathf.Max(0.1f, damageMultiplier);
    public float Speed => Mathf.Max(1f, speed) * Mathf.Max(0.1f, speedMultiplier);
    public float LifeTime => Mathf.Max(0.1f,lifeTime) * Mathf.Max(0.01f, lifeTimeMultiplier); //子弹的最小存在时长: 0.1s
    public Vector2 Position => basePosition + offsetPosition;
    public Quaternion Rotation
    {
        get  //子弹旋转角由三部分组成: 基础旋转角(由发射器给出) + 固定偏移角(由某些阵型卡牌设置) + 随机偏移角(取决于randomAngle)
        {
            float angle = Mathf.Max(0, randomAngle); //当随机偏移角为负时，相当于子弹被修正精确，将随机偏移量置为0
            Quaternion randomRotation = Quaternion.Euler(Vector3.forward * Random.Range(-angle,angle));
            return baseRotation * offsetRotation * randomRotation;
        }
    }
    public Vector2 Scale => Vector2.one * Mathf.Max(0.2f, scale);
    public float HitBack => hitback;
    public int Penetrate => penetrate;

    // public bool HasAbility(BulletAbility ability)
    // {
    //     return (this.ability & (int)ability) != 0;
    // }
    public bool HasChasing => (ability & (int)BulletAbility.Chasing) != 0;
    public bool HasPenetrate => (ability & (int)BulletAbility.Penetrate) != 0;
    
    #endregion

    #region 外部修改时调用函数
    public void AddAbility(BulletAbility ability) { this.ability |= (int)ability; }
    public void ModifyDamage(float amount) { damage += damage + amount; }
    public void ModifyDamageMultiplier(float amount) { damageMultiplier = damageMultiplier + amount;  }
    public void ModifySpeed(float amount) { speed = speed + amount; }
    public void ModifySpeedMultiplier(float amount){speedMultiplier = speedMultiplier + amount;}
    public void ModifyLifeTime(float amount) {lifeTime = lifeTime + amount;}
    public void ModifyLifeTimeMultiplier(float amount) { lifeTimeMultiplier = lifeTimeMultiplier + amount; }

    public void SetBase(Vector2 position, Quaternion rotation) 
    {
        this.basePosition = position; 
        this.baseRotation = rotation;
    }
    public void AddOffset(Vector2 offsetPosition, float angle)
    {
        this.offsetPosition += (Vector2)(baseRotation * offsetPosition);
        var temp = Quaternion.Euler(Vector3.forward * angle);
        this.offsetRotation *= temp;
    }
    
    public void ModifyScale(float amount)
    {
        scale += amount;
    }
    public void ModifyRandomAngle(float amount)
    {
        randomAngle += amount;
    }
    public void ModifyHitback(float amount)
    {
        hitback += amount;
    }
    public void ModifyPenetrate(int amount)
    {
        penetrate += amount;
    }
    #endregion

    
}
