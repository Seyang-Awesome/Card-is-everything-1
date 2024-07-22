using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGamePrefabs",menuName ="ScriptableObjects/Data/PrefabCenter")]
public class PrefabCenter : ScriptableObject
{
    [Header("卡牌")]
    public CardFace cardFace; //卡面模板

    [Header("子弹")]
    public Bullet gunBullet;
    public Bullet arrowBullet;

    [Header("粒子")]
    public ParticleSystem cardParticle;
    public ParticleSystem gunBulletParticle;
    public ParticleSystem flashParticle;

    [Header("敌人")]
    public Enemy simpleEnemy;
}

