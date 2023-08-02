using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.mainScriptContainer.UI.Buttons
{
    public class MuteBackgroundMusicButton : CustomSpriteButton
    {
        protected override void Start()
        {
            base.Start();
            //will change the music player button based on musicManager canplaybackground volume boolen 
            if (MusicManager.Instance.CanPlayBackgroundVolume)
            {
                //will make the sprite clickable
                image.sprite = normalSprite;
            }
            else
            {
                //will show that the sprite is already click
                image.sprite = clickSprite;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //just showing the animation when the button is hold down
            image.sprite = clickSprite;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
            MusicManager.Instance.ToggleCanPlayBackgroundMusic();

            if (MusicManager.Instance.CanPlayBackgroundVolume)
            {//same thing as in the start method
                image.sprite = normalSprite;
            }
            else
            {
                image.sprite = clickSprite;
            }
        }
    }
}