using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public PrefabCenter prefabCenter; //预制体中心，存储所有游戏中常用预制体
    public BuffCenter buffCenter; //Buff中心，存储游戏中所有buff
}
