using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;


public class DeleteItems : IMouseStates
{
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        ListenAndDeleteSelectedItem(mouseBehaviour);
        if (mouseBehaviour.deleteActivated)
        {
            return mouseBehaviour.deleteItems;
        }
        else
        {
            return mouseBehaviour.mouseIdle;
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


            if(collidedObject.collider != null)
            {
                foreach(ItemButton itemButton in mouseBehaviour.itemButtons)
                {
                    if (itemButton.IsGameObjectRelated(collidedObject.collider.gameObject))
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
