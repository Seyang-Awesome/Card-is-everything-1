using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashCard", menuName = "ScriptableObjects/Card/FlashFire")]
public class Shooter_Flash_Card : Card
{
    [Header("卡牌特有属性")]
    [SerializeField] 
    private float maxDistance = 5f;
    [SerializeField] 
    private float duration = 5f;

    public override void Use()
    {
        base.Use();

        var t = Player.Instance.transform;
        var dir = (MyExtensions.MousePosition - (Vector2)t.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);
        EffectManager.Instance.GenerateParticle(DataManager.Instance.prefabCenter.flashParticle, t.position, rotation);

        t.position += (Vector3)dir * maxDistance;

        BuffManager.Instance.ApplyBuff<Pensentrate>(duration);
    }
}
