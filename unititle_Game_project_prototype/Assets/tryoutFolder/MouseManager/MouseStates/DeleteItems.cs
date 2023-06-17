using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DeleteItems : IMouseStates
{
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        ListenAndDeleteSelectedItem(mouseBehaviour);
        ListenForUIClick(mouseBehaviour);
        if (mouseBehaviour.deleteActivated)
        {
            return mouseBehaviour.deleteItems;
        }
        else
        {
            return mouseBehaviour.mouseIdle;
        }
    }

    private void ListenForUIClick(MouseBehaviour mouseBehaviour)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        string deletebuttonName = "Delete button";
        foreach (var raycastResult in raysastResults)
        {
            if (raycastResult.gameObject.name == deletebuttonName)
            {
                mouseBehaviour.deleteActivated = false;
            }
        }

    }

    private void ListenAndDeleteSelectedItem(MouseBehaviour mouseBehaviour)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float minDept = LayerManager.Current.GetGearZIndexBasedOnCurrentLayer() - 0.5f;
            float maxDept = LayerManager.Current.GetGearZIndexBasedOnCurrentLayer() + 0.5f;
            var collidedObject = Physics2D.Raycast(positionOfMouse,
                Vector2.zero,
                float.PositiveInfinity,
                LayerData.MoveableGearLayer,
                minDept,
                maxDept
            );

            if(collidedObject != null)
            {
                foreach(ItemButton itemButton in mouseBehaviour.itemButtons)
                {
                    if (itemButton.IsGearRelated(collidedObject.collider.gameObject))
                    {
                        itemButton.RemoveItem(collidedObject.collider.gameObject);
                        break;
                    }
                }
            }
        }
    }

    //probably a o(n) operation 

}
