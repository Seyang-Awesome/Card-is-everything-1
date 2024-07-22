using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可被对象池处理的物体，应当在进出对象池时调用该接口内容
/// </summary>
public interface IPoolable
{
    public void OnPushToPool();
    public void OnGetFromPool();
}
