using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseIdle : IMouseStates 
    {
    //Readme
    /*
    The mouse idle state is as you guess, a state that does not do anything.
    It will keep track and see what the player do and notice if there is any
    change in state (like for instance if the delete state is activated).
    It checks for clicks and raycast them (both UI and Gameobject). Do check
    those functions.
    */
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // will always see a mouse click. it is, it will
            //do a ray cast to see what it hit.
            RayCastUI(mouseBehaviour);
            RayCastGameObject(mouseBehaviour);
            if (mouseBehaviour.selectedObject != null)
            {
                //if there is selected object, the state switches to move the selected object
                return mouseBehaviour.mouseMoveSelectedObject;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else if (mouseBehaviour.deleteActivated) //if nothing happen and suddenly the delete activated bool is true, switch to delete mode
        {
            //the deleteactivated is toggled at the delete button 
            return mouseBehaviour.deleteItems;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void RayCastGameObject(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float minDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() - 0.5f;
        float maxDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() + 0.5f;
        //The z index restriction is to restrict the raycast to search the current layer

        var collidedObject = Physics2D.Raycast(positionOfMouse, 
            Vector2.zero,
            float.PositiveInfinity,
            LayerData.MoveableItemLayer,
            minDept,
            maxDept
            ); 

        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
    

        if (collidedObject.collider != null)
        { //if it has a collider, it means the raycast hits a moveablecomponet and that the mouse idle can extract out the 
            //Imoveable component from it so that it can move it
            mouseBehaviour.selectedObject = mouseBehaviour.GetImoveableComponent(collidedObject.collider);
        }
    }

    private void RayCastUI(MouseBehaviour mouseBehaviour)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        /*Use event system component to raycast the UI component 
          to find if the player hits a Itembutton. 
        */
        foreach (var raycastResult in raysastResults)
        {
            if (raycastResult.gameObject.TryGetComponent<ItemButton>(out ItemButton manager))
            {//will scan through each result and find if the component is an ItemButton
                if (manager.CanBuyItem())
                {
                    GameObject gear = manager.GetItem(); //get Item from pool
                    mouseBehaviour.selectedObject = gear.GetComponent<IMoveable>(); //make it the selected object               

                    var newPosition = mouseBehaviour.GetVector3FromMousePosition(); 
                    mouseBehaviour.selectedObject.Move(newPosition); 
                    //the selectedobject have to move at the place where the mouse is at 
                    //as when activated, it can be at a random position. 
                    //doing this will make sure it will move to the mouse position.
                }
            }
        }
    }

}
