using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : CustomButton
{
    // readme
    /*
        this is done at unity so you wont see
    anything that affect the variables. What this script does
    is just toggle of and on. For some reason this does not work with
    normal regular button becuase I did not like how they 
    turn off the active state. 
    */
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


