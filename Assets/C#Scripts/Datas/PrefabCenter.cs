using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InGamePrefabs",menuName ="ScriptableObjects/Data/PrefabCenter")]
public class PrefabCenter : ScriptableObject
{
    [Header("卡牌")]
    public CardFace cardFace; //卡面模板

    [FormerlySerializedAs("gunBullet")] [Header("子弹")]
    public BulletDependency gunBulletDependency;
    [FormerlySerializedAs("arrowBullet")] public BulletDependency arrowBulletDependency;

    [Header("粒子")]
    public ParticleSystem cardParticle;
    public ParticleSystem gunBulletParticle;
    public ParticleSystem flashParticle;

    [Header("敌人")]
    public Enemy simpleEnemy;
}

