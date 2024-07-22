using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buffs", menuName = "ScriptableObjects/Data/BuffCenter")]
public class BuffCenter : ScriptableObject
{
    public List<Buff> buffs;
}
