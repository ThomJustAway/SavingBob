using Assets.Scripts.mainScriptContainer.UI.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : CustomSpriteButton
{
    /* Level button script is like a scene button script but with certain restrictions
     
       it will check for whether players have completed a level. If they do, they can move on to the next level
   
        level 1: 2
        level 2: 3
        level 3: 4
        level 4: 5
     */

    private bool disabled = false;
    [SerializeField] private int levelInCharge = 2; // this will be change in unity editor (2 is just a place holder)
    public static string Key { get { return "playerLevel"; } }

    private void OnEnable()
    {
        if(image == null)
        {
            image = GetComponent<Image>(); // For some reason this does not work on the start function so I had to call this here
        }
        
        checkIfDisabled(PlayerPrefs.GetInt(Key)); //see if the condition is met

        if (disabled) //show visuals to show that it is disabled
        {
            image.color = Color.grey;
        }
        else 
        {
            image.color = Color.white;
        }
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!disabled)
        {
            image.sprite = clickSprite;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!disabled)
        {
            image.sprite = normalSprite;
            MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
            SceneTransitionManager.instance.StartTransition(levelInCharge); 
            // make sure it can transition to different scene when click
        }
    }

    private void checkIfDisabled(int level)
    {
        disabled = level < levelInCharge;

        //Level = current Level the player completed
        // this will check if the Level that they completed is more of equal to the level the button is incharge
        // if it is, disabled is false, else it is true
        // 2 < 2

    }
}
