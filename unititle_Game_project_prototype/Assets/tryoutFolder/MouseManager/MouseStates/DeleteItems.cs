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
        Debug.Log("On Delete state!");
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
                Debug.Log(collidedObject.collider.name);
                foreach(ItemButton itemButton in mouseBehaviour.itemButtons)
                {
                    Debug.Log(itemButton.name);
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
