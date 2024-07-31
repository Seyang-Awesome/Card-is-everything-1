using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 子弹的依赖脚本，仅负责移动，检测碰撞逻辑
/// </summary>
public sealed class BulletDependency : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer trail;
    private IEntity source;
    private int targetLayer;
    
    public event Action<HitInfo> onHit;
    // public Action<HitInfo> onHitWall;

    public void Init(IEntity source, int targetLayer)
    {
        this.source = source;
        this.targetLayer = targetLayer;
    }

    public void ResetSelf()
    {
        onHit = null;
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
        EntityDependency entityDependency = entityComponent as EntityDependency;
        if ((targetLayer & (int)entityDependency.entityLayer) != 0)
        {
            // TODO：完善HitInfo的碰撞角度
            onHit?.Invoke(new HitInfo(rb.velocity, source, entityDependency.entity));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            onHit?.Invoke(new HitInfo(rb.velocity, true));
    }
}

