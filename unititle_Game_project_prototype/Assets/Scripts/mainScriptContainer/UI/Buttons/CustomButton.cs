using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public abstract class CustomButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //custom button script that helps to change the color of the button. different from custom sprite button script
    [SerializeField] protected Color ClickColor;
    [SerializeField] protected Color HoverColor;
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color ActivatedColor;
    protected Image image;
    protected virtual void Start()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        image.color = ClickColor;
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
    }


    public abstract void OnPointerExit(PointerEventData eventData);



    public abstract void OnPointerUp(PointerEventData eventData);


}
