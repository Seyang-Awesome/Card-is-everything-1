using System;
using System.Collections.Generic;
using UnityEngine;

public enum EntityLayer
{
    Player = 0x001,
    Enemy = 0x002,
}
public class EntityDependency : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public IEntity entity;
    public EntityLayer entityLayer;
    public InvokableAction<HitInfo> onHit;

    public void Init(IEntity entity)
    {
        this.entity = entity;
    }
    

    
}

