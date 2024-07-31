using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class BulletDependency : MonoBehaviour
{
    public int targetLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer trail;

    public event Action<BulletDependency> onSpawn;
    public event Action<BulletDependency> onDespawn;
    public event Action<EntityDependency> onHitEntity;
    public event Action onHitWall;
    
    public void Spawn(Vector2 velocity, int targetLayer)
    {
        this.targetLayer = targetLayer;
        ScheduleManager.Instance.AddSchedule(new Schedule(info.LifeTime, () => { if (gameObject.activeSelf) OnDisappear(); }));
        onSpawn?.Invoke(this);
    }
    
    public void Despawn()
    {
        PoolManager.Instance.PushGameObject(gameObject); 
        EffectManager.Instance.GenerateParticle(DataManager.Instance.prefabCenter.gunBulletParticle, transform.position, Quaternion.identity);
        onDespawn?.Invoke(this);
    }
    
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetBulletTrailWidth(float width)
    {
        trail.startWidth = width;
        trail.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(typeof(EntityDependency), out Component entityComponent)) return;
        EntityDependency entity = entityComponent as EntityDependency;
        if ((targetLayer & (int)entity.entityLayer) != 0)
        {
            onHitEntity?.Invoke(entity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            onHitWall?.Invoke();
    }
    
    public virtual void Launch(BulletInfo info) //发射初始化函数
    {
        //设定两个延时事件: 一定时间后摧毁子弹并创建粒子效果，这里并不完善，可以优化
        
        this.info = info;
        transform.position = info.Position;
        transform.rotation = info.Rotation;
        transform.localScale = info.Scale;
        Trail.startWidth = Consts.bulletTrailWidth * info.Scale.x;
        Trail.Clear();
    }
    
}

