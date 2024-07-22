using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Bullet : MonoBehaviour,IPoolable
{
    private Rigidbody2D rb;
    protected Rigidbody2D Rb
    {
        get
        { 
            if(rb == null) 
                rb = GetComponentInChildren<Rigidbody2D>();
            return rb;
        }
    }
    private TrailRenderer trail;
    protected TrailRenderer Trail
    {
        get
        {
            if (trail == null)
                trail = GetComponentInChildren<TrailRenderer>();
            return trail;
        }
    }
    protected BulletInfo info;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var e = collision.gameObject.GetComponent<Enemy>();
            OnHitEnemy(e);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            OnHitWall();
    }


    protected virtual void OnHitEnemy(Enemy enemy)
    {

    }

    protected virtual void OnHitWall()
    {

    }

    public virtual void Launch(BulletInfo info) //发射初始化函数
    {
        //设定两个延时事件: 一定时间后摧毁子弹并创建粒子效果，这里并不完善，可以优化
        ScheduleManager.Instance.AddSchedule(new Schedule(info.LifeTime, () => { if (gameObject.activeSelf) OnDisappear(); }));
        this.info = info;
        transform.position = info.Position;
        transform.rotation = info.Rotation;
        transform.localScale = info.Scale;
        Trail.startWidth = Consts.bulletTrailWidth * info.Scale.x;
        Trail.Clear();
    }

    public virtual void OnDisappear()
    {   
        PoolManager.Instance.PushGameObject(gameObject); 
        EffectManager.Instance.GenerateParticle(DataManager.Instance.prefabCenter.gunBulletParticle, transform.position, Quaternion.identity);
    }

    public virtual void OnPushToPool()
    {
        
    }

    public virtual void OnGetFromPool()
    {
        
    }
}

