using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    //readme
    /*
    This is the button for restarting.
    What this does is that it will 
    get call the itembutton.returnallgameobject function
    to return all the gameobject in the game.
    */
    public void ResetLevel()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        MoneyManager.instance.ResetMoney();
        for(int i  = 0; i < LevelManager.instance.itemButtons.Length; i++)
        {// get each itembutton to keep all the items
            LevelManager.instance.itemButtons[i].ReturnAllGameObject();
        }
    }

}
