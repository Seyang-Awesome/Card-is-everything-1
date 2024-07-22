using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
    [SerializeField]
    private PlayerInfo info;
    private PlayerController controller;
    private PlayerStatManager statManager;
    private WeaponHolder holder;

    public PlayerInfo Info => info;
    public PlayerStatManager StatManager => statManager;

    private void Start()
    {
        Init(); //初始化
    }

    private void Init()
    {
        controller = new(this);
        info = Instantiate(info);
        holder = GetComponentInChildren<WeaponHolder>();
        holder.Init(this);
        statManager = new PlayerStatManager();
        statManager.Init(this);
    }

    protected override void PauseableUpdate()
    {
        controller.OnUpdate();
        statManager.OnUpdate();
    }
}
