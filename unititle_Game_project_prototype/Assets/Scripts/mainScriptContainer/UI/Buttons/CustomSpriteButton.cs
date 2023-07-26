using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Scripts.mainScriptContainer.UI.Buttons
{
    public abstract class CustomSpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected Sprite normalSprite;
        [SerializeField] protected Sprite hoveredSprite;
        [SerializeField] protected Sprite clickSprite;
        [SerializeField] protected Sprite disabledSprite;
        protected Image image;
        protected virtual void Start()
        {
            image = GetComponent<Image>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }


        public virtual void OnPointerEnter(PointerEventData eventData)
        {
        }


        public virtual void OnPointerExit(PointerEventData eventData) { }



        public virtual void OnPointerUp(PointerEventData eventData) { }
    }
}