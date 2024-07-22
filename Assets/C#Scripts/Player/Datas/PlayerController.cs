using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    #region 字段
    private Player player;
    private Rigidbody2D rb;
    #endregion

    public PlayerController(Player player)
    {
        this.player = player;
        this.rb = player.GetComponent<Rigidbody2D>();
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

        rb.velocity = player.Info.Speed * dir;
    }
}
