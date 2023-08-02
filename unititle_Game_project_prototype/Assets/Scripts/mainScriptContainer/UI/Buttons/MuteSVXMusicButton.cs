using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.mainScriptContainer.UI.Buttons
{

    public class MuteSVXMusicButton : CustomSpriteButton
    {
        protected override void Start()
        {
            base.Start();
            //will change the sprite according to the canplaySVXVolume boolen
            if (MusicManager.Instance.CanPlaySvxVolume)
            {//will make the sprite clickable
                image.sprite = normalSprite;
            }
            else
            {//show that it is already click
                image.sprite = clickSprite;
            }

        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //animation to show that it is click when mouse is down
            image.sprite = clickSprite;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
            MusicManager.Instance.ToggleCanPlaySVXMusic();
            // same thing as the start method
            if (MusicManager.Instance.CanPlaySvxVolume)
            {
                image.sprite = normalSprite;
            }
            else
            {
                image.sprite = clickSprite;
            }
        }
    }
}