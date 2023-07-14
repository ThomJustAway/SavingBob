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


