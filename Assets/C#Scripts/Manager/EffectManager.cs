using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    /// <summary>
    /// 创建一个粒子效果
    /// </summary>
    /// <param name="particle"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    public void GenerateParticle(ParticleSystem particle,Vector3 position,Quaternion rotation,Transform parent = null)
    {
        float duration = particle.main.duration;
        var go = PoolManager.Instance.GetGameObject(particle.gameObject, position, rotation, parent);
        ScheduleManager.Instance.AddSchedule(new Schedule(duration,() => PoolManager.Instance.PushGameObject(go)));
    }
}
