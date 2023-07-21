using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{

    private ItemButton[] buttons;

    private void Awake()
    {
        GameManager.instance.FinishCreatingGearButtonEvent += GetButtons;
    }

    public void ResetLevel()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        MoneyManager.instance.ResetMoney();
        for(int i  = 0; i < buttons.Length; i++)
        {
            buttons[i].ReturnAllGameObject();
        }
    }

    private void GetButtons(ItemButton[] obj)
    {
        buttons = obj;
        GameManager.instance.FinishCreatingGearButtonEvent -= GetButtons;
    }

}
