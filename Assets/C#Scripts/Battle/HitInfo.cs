using System;
using System.Collections.Generic;
using UnityEngine;

public struct HitInfo
{
    public bool isWall;
    public Vector2 velocity;
    public IEntity source;
    public IEntity target;

    public HitInfo(Vector2 velocity, bool isWall, IEntity source = null, IEntity target = null)
    {
        this.isWall = isWall;
        this.velocity = velocity;
        this.source = source;
        this.target = target;
    }

    public HitInfo(Vector2 velocity, IEntity source, IEntity target, bool isWall = false)
    {
        this.isWall = isWall;
        this.velocity = velocity;
        this.source = source;
        this.target = target;
    }
    
}

