using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 标准的子弹类，可以尝试做别的子弹，但我现在就做了这一个~
/// </summary>
public class SingleBullet : Bullet
{
    private int pensentrateCount = 0;

    public void Launch(BulletInfo info)
    {
        base.Launch(info);
        Rb.velocity = transform.right * info.Speed;
    }

    protected void OnHitEnemy(Enemy e)
    {
        base.OnHitEnemy(e);

        e.Dagame(info.Damage, e.transform.position - transform.position, info.HitBack);
        EffectManager.Instance.GenerateParticle(DataManager.Instance.prefabCenter.gunBulletParticle, transform.position, Quaternion.identity);

        //打中敌人后添加1层穿透计数，当穿透计数超过原有穿透上限时，移除子弹
        if (pensentrateCount++ >= info.Penetrate && !info.HasAbility(BulletAbility.Penetrate))
            OnDisappear();
    }
}
