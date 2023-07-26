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
            if (MusicManager.Instance.CanPlayBackgroundVolume)
            {
                image.sprite = normalSprite;
            }
            else
            {
                image.sprite = clickSprite;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            image.sprite = clickSprite;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
            MusicManager.Instance.ToggleCanPlayBackgroundMusic();
            if (MusicManager.Instance.CanPlayBackgroundVolume)
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