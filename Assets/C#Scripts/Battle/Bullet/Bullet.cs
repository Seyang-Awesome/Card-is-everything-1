using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 子弹的整合脚本，管理整个子弹对象的
/// 1. 依赖
/// 2. 子弹信息
/// 3. 子弹效果
/// </summary>
public sealed class Bullet : MonoBehaviour
{
    [field:SerializeField] public BulletDependency Dependency { get; private set; }
    private BulletInfo info;
    private List<BulletEffect> effects;
    
    private Action onSpawn;
    private Action onDespawn;
    private Action onUpdate;
    private Action<HitInfo> onHit;
    
    public void Spawn(BulletInfo info, IEnumerable<BulletEffect> bulletEffects)
    {
        ResetSelf();
        
        this.info = info;
        Dependency.Init(info.Source, info.TargetLayer);
        effects = bulletEffects.ToList();
        foreach (var effect in effects)
        {
            AddEffect(effect);
        }
        
        // ScheduleManager.Instance.AddSchedule(new Schedule(info.LifeTime, () => { if (gameObject.activeSelf) OnDisappear(); }));
        onSpawn?.Invoke();
    }
    
    public void Despawn()
    {
        PoolManager.Instance.PushGameObject(gameObject); 
        EffectManager.Instance.GenerateParticle(DataManager.Instance.prefabCenter.gunBulletParticle, transform.position, Quaternion.identity);
        onDespawn?.Invoke();
    }

    private void Update()
    {
        onUpdate?.Invoke();
    }

    private void ResetSelf()
    {
        info = null;
        effects = null;
        onSpawn = null;
        onDespawn = null;
        Dependency.ResetSelf();
    }

    public void AddEffect(BulletEffect effect)
    {
        effect.Init(Dependency, info);
        onSpawn += effect.OnSpawn;
        onDespawn += effect.OnDespawn;
        Dependency.onHit += effect.OnHit;
        onUpdate += effect.OnUpdate;
        effect.onRequestRemoveSelf += RemoveEffect;
    }

    public void RemoveEffect(BulletEffect effect)
    {
        effects.Remove(effect);
    }
}

