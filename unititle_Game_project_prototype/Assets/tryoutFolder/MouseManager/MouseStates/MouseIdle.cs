using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
                return mouseBehaviour.mouseDragGear;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void RayCastGameObject(MouseBehaviour mouseBehaviour)
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
        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
        //The z index restriction is to prevent gears from top or bottom from mixing together
        //need to add code to get different things
        if (collidedObject.collider != null)
        {
            //GetType(collidedObject.collider);
            Transform hitGameObject = collidedObject.collider.transform;
            mouseBehaviour.selectedObject = hitGameObject;
        }
    }

    private void RayCastUI(MouseBehaviour mouseBehaviour)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        //this code only works for making Gear
        foreach(var raycastResult in raysastResults)
        {
            //do code that make the gear follow.
            //if gear is not place in scene, hide the gear using Gearpool.
            if (raycastResult.gameObject.TryGetComponent<GearButton>(out GearButton manager))
            {
                GameObject gear = manager.GetGear(); //get gear from pool
                mouseBehaviour.selectedObject = gear.transform; //make it the selected object

                Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = mouseBehaviour.grid.WorldToCell(positionOfMouse);
                cellPosition.z = LayerManager.Current.GetGearZIndexBasedOnCurrentLayer();
                mouseBehaviour.selectedObject.position = mouseBehaviour.grid.GetCellCenterLocal(cellPosition);
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
