using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 标准敌人，持续追踪，碰撞伤害
/// </summary>
public class SimpleEnemy : Enemy
{
    protected override void PauseableUpdate()
    {
        base.PauseableUpdate();

        hitbackTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        var dir = Player.Instance.transform.position - transform.position;

        if(hitbackTimer < 0)
            rb.velocity = dir.normalized * info.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var p = collision.gameObject.GetComponent<Player>();
            p.StatManager.TakeHit(info.attack);
        }
    }
}
