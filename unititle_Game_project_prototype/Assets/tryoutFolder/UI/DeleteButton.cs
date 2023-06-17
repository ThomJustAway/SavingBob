using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color HoverColor;
    [SerializeField] private Color ClickColor;
    [SerializeField] private Color ActivatedColor;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
        print("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
        print("exit");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = ClickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = ActivatedColor;
    }
}
