using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;


public class DeleteItems : IMouseStates
{
    //readme
    /*
    This state does one thing. listen for click and see if 
    there is a Imoveable object. You can read abit on how the 
    itembutton works but how this state works is by having a stored 
    array of itembutton (in mousebehaviour) and bringing it back to the pool

    it will return back to idle state if it sense the deleteActivated bool to 
    be false.
    */
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        ListenAndDeleteSelectedItem(mouseBehaviour);
        if (mouseBehaviour.deleteActivated)
        {//if the delete activated is still active, then continue with the delete state
            return mouseBehaviour.deleteItems;
        }
        else
        { //else reuturn back to the mouse idel state
            return mouseBehaviour.mouseIdle;
        }
    }

    private void ListenAndDeleteSelectedItem(MouseBehaviour mouseBehaviour)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mb magic int this is the range for detecting the layer
            float minDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() - 0.5f;
            float maxDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() + 0.5f; 
            //restrict the raycast to only the current layer

            var collidedObject = Physics2D.Raycast(positionOfMouse,
                Vector2.zero,
                float.PositiveInfinity,
                LayerData.MoveableItemLayer,
                minDept,
                maxDept
            );

            if(collidedObject.collider != null)
            { //if it found a collider, it mean it is a IMoveable. Therefore delete the Imoveabl component
                IMoveable ImoveableComponent = mouseBehaviour.GetImoveableComponent(collidedObject.collider);
                foreach (ItemButton itemButton in LevelManager.instance.itemButtons)
                {//find the itembutton and then remove the selected Imoveable component
                    if (itemButton.IsGameObjectRelated(ImoveableComponent.Getprefab))
                    {
                        itemButton.RemoveItem(ImoveableComponent.Getprefab);
                        break;
                    }
                }
            }
        }
    }

    //it is an o(n) operation which I am not too happy about

}
