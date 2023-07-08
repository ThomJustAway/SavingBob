using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : CustomButton
{
    private MouseBehaviour mouseBehaviour;
    //public UnityEvent isClicked = new UnityEvent(); 
    //Maybe add a unity event for the delete button since you want to add a tooltip to appear to show that it has been clicked!

    //private  void Start()
    //{
    //    image = GetComponent<Image>();
    //    image.color = normalColor;
    //    mouseBehaviour = FindObjectOfType<MouseBehaviour>();
    //}
    protected override void Start()
    {
        base.Start();
        mouseBehaviour = FindObjectOfType<MouseBehaviour>();
    }

    public override void OnPointerExit(PointerEventData eventData)
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


    public override void OnPointerUp(PointerEventData eventData)
    {
        mouseBehaviour.ToggleDeletedActiavted();
        image.color = HoverColor;
    }
}


