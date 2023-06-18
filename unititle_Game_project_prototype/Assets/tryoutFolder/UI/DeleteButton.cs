using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler 
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color HoverColor;
    [SerializeField] private Color ClickColor;
    [SerializeField] private Color ActivatedColor;

    private Image image;
    private MouseBehaviour mouseBehaviour;
    //public UnityEvent isClicked = new UnityEvent(); 
    //Maybe add a unity event for the delete button since you want to add a tooltip to appear to show that it has been clicked!

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
        mouseBehaviour = FindObjectOfType<MouseBehaviour>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseBehaviour.deleteActivated)
        {
            image.color = ActivatedColor;
        }
        else
        {
            image.color = normalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = ClickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseBehaviour.ToggleDeletedActiavted();
        //isClicked?.Invoke();
        image.color = HoverColor;
    }
}
