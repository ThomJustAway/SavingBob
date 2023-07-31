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
            if (MusicManager.Instance.CanPlaySvxVolume)
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
            MusicManager.Instance.ToggleCanPlaySVXMusic();
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