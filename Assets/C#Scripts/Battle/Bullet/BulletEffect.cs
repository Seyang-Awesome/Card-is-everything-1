using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹效果基类，提供部分回调方法供使用
/// </summary>
public abstract class BulletEffect
{
    protected BulletDependency dependency;
    protected BulletInfo info;
    public event Action<BulletEffect> onRequestRemoveSelf;

    public void Init(BulletDependency dependency, BulletInfo info)
    {
        this.dependency = dependency;
        this.info = info;
    }
    
    public void RemoveSelf()
    {
        onRequestRemoveSelf?.Invoke(this);
    }
    
    public abstract void OnSpawn();
    public abstract void OnDespawn();
    public abstract void OnHit(HitInfo hitInfo);
    public abstract void OnUpdate();
}

