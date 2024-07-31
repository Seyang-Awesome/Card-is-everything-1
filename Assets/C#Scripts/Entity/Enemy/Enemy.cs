using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人脚本，这部分写的比较粗糙，仅作为实现测试用
/// </summary>
public class Enemy : ModifiedMonobehaviour, IPoolable, IEntity
{
    [SerializeField]
    protected EnemyInfo info;

    protected Rigidbody2D rb;

    protected float hitbackTimer; //击退计时器，被击退时一个短暂的时间内停止追踪

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Spawn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var p = collision.gameObject.GetComponent<Player>();
            Attack(p);
        }
    }

    /// <summary>
    /// 从对象池被取出时
    /// </summary>
    protected virtual void Spawn()
    {
        //此处应该进行对敌人每次生成的处理，即给予一组随时间增长的初始化数据，但有点麻烦，这边懒得写了，临时测试的，要改。
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f);
        info.health = 10;
        info.speed = 3;
        info.attack = 2;
    }

    /// <summary>
    /// 攻击玩家时
    /// </summary>
    /// <param name="p"></param>
    protected virtual void Attack(Player p)
    {
        p.StatManager.TakeHit(info.attack);
    }

    /// <summary>
    /// 受到无方向伤害时(如DOT)
    /// </summary>
    /// <param name="damage"></param>
    public virtual void Damage(float damage) //受伤函数，无来源
    {
        Dagame(damage, Vector2.zero, 0);
    }

    /// <summary>
    /// 受到一个有方向的伤害时
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="dir"></param>
    /// <param name="strength"></param>
    public virtual void Dagame(float damage, Vector2 dir, float strength) //受伤函数，有击退方向
    {
        
        info.health -= damage;
        if (info.health < 0)
            Die();

        rb.velocity = dir.normalized * strength;
        hitbackTimer = Consts.hitbackTimer;
    }

    protected virtual void Die() //死亡时调用
    {
        PoolManager.Instance.PushGameObject(gameObject);
    }

    public void OnPushToPool()
    {
        
    }

    public void OnGetFromPool()
    {
        Spawn();
    }

    public EntityInfoBase GetInfo()
    {
        return info;
    }
}
