using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : Singleton<GlobalManager>
{
    protected override bool IsDonDestroyOnLoad => true;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Screen.SetResolution(1920, 1080, !Screen.fullScreen);
        }
    }
}
