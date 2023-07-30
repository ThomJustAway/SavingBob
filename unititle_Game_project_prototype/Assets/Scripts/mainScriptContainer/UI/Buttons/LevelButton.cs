using Assets.Scripts.mainScriptContainer.UI.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : CustomSpriteButton
{
    private bool disabled = false;
    [SerializeField] private int sceneInCharge = 2;
    public static string Key { get { return "playerLevel"; } }


    private void OnEnable()
    {
        if(image == null)
        {
            image = GetComponent<Image>(); 
        }
        
        checkIfDisabled(PlayerPrefs.GetInt(Key));

        if (disabled)
        {
            image.color = Color.grey;
        }
        else {
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
            SceneTransitionManager.instance.StartTransition(sceneInCharge);
        }
    }

    private void checkIfDisabled(int scene)
    {
        disabled = scene < sceneInCharge;
        
    }
}
