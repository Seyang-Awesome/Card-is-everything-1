using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Player : Singleton<Player>, IEntity
{
    [SerializeField] private PlayerInfo info;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField] private Rigidbody2D rb;
    private PlayerController controller;
    private PlayerStatManager statManager;

    [SerializeField] private PlayerInfoConfig config;
    public PlayerInfo Info => info;
    public PlayerStatManager StatManager => statManager;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        info = new PlayerInfo();
        weaponHolder.Init(info, transform);
        controller = new(info, rb);
        statManager = new PlayerStatManager(info);
    }

    protected override void PauseableUpdate()
    {
        controller.OnUpdate();
        statManager.OnUpdate();
    }

    public EntityInfoBase GetInfo()
    {
        return info;
    }
}
