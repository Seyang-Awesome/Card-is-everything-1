using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public Card card;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            card.Use();
        if (Input.GetKeyDown(KeyCode.E))
            BuffManager.Instance.ApplyBuff<ScaleUp>(3);
        if (Input.GetKeyDown(KeyCode.R))
            BuffManager.Instance.ApplyBuff<StopShooting>(3);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CardManager.Instance.TryUse(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CardManager.Instance.TryUse(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CardManager.Instance.TryUse(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            CardManager.Instance.TryUse(3);
    }
}
