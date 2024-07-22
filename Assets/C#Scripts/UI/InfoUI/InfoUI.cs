using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : ModifiedMonobehaviour
{
    [SerializeField]
    private Slider healthBar, healthBuffer , manaBar, manaBuffer, expBar;

    [SerializeField]
    private Text healthText, manaText;

    private PlayerInfo info;

    private void Start()
    {
        info = Player.Instance.Info;
    }

    protected override void PauseableUpdate()
    {
        base.PauseableUpdate();

        healthBar.value = info.CurrentHealth / info.MaxHealth;
        manaBar.value = info.CurrentMana / info.MaxMana;
        expBar.value = info.Exp / info.MaxExp;

        healthBuffer.value = Mathf.MoveTowards(healthBuffer.value, healthBar.value, Time.deltaTime);
        manaBuffer.value = Mathf.MoveTowards(manaBuffer.value, manaBar.value, Time.deltaTime);

        healthText.text = (int)info.CurrentHealth + "/" + (int)info.MaxHealth;
        manaText.text = (int)info.CurrentMana + "/" + (int)info.MaxMana;
    }
}
