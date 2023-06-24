using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseIdle : IMouseStates 
    {
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCastUI(mouseBehaviour);
            RayCastGameObject(mouseBehaviour);
            if (mouseBehaviour.selectedObject != null)
            {
                //Debug.Log(mouseBehaviour.selectedObject);
                return mouseBehaviour.mouseMoveSelectedObject;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else if (mouseBehaviour.deleteActivated)
        {
            return mouseBehaviour.deleteItems;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void RayCastGameObject(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float minDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() - 0.5f;
        float maxDept = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() + 0.5f;
        var collidedObject = Physics2D.Raycast(positionOfMouse, 
            Vector2.zero,
            float.PositiveInfinity,
            LayerData.MoveableGearLayer,
            minDept,
            maxDept
            ); 

        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
        //The z index restriction is to prevent gears from top or bottom from mixing together
        //need to add code to get different things


        //problem now is that only the bottom part of the joint can be move.
        // the top part of the joint cant be move.
        if (collidedObject.collider != null)
        {
            mouseBehaviour.selectedObject = mouseBehaviour.GetImoveableComponent(collidedObject.collider);
        }
    }

    private void RayCastUI(MouseBehaviour mouseBehaviour)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        //this code only works for making Gear
        foreach (var raycastResult in raysastResults)
        {
            //do code that make the gear follow.
            //if gear is not place in scene, hide the gear using Gearpool.
            if (raycastResult.gameObject.TryGetComponent<ItemButton>(out ItemButton manager))
            {
                if (manager.CanBuyItem())
                {
                    GameObject gear = manager.GetItem(); //get Item from pool
                    mouseBehaviour.selectedObject = gear.GetComponent<IMoveable>(); //make it the selected object               
                
                    Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPosition = mouseBehaviour.grid.WorldToCell(positionOfMouse);
                    cellPosition.z = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer();
                    mouseBehaviour.selectedObject.Move(cellPosition);
                }
            }


        }
    }

    //private System.Type[] existingSystemType = { typeof(Gear), typeof(JointBehaviour) }; 

    //private void GetType(Collider2D collider)
    //{
    //    foreach(System.Type type in existingSystemType)
    //    {
    //        if(collider.TryGetComponent(type, out Component compenent))
    //        {
    //            Debug.Log(compenent.gameObject.name);
    //            return;
    //        }
    //    }
    //    Debug.Log("no avaliable component detected");
    //}
}
