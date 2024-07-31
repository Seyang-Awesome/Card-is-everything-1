using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerInfo info;
    private Rigidbody2D rb;

    public PlayerController(PlayerInfo info, Rigidbody2D rb)
    {
        this.info = info;
        this.rb = rb;
    }

    public void OnUpdate()
    {
        Move();
    }

    /// <summary>
    /// 玩家移动函数
    /// </summary>
    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(hor, ver);
        dir = Vector2.ClampMagnitude(dir, 1.0f);

        rb.velocity = info.Speed * dir;
    }
}
