using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseIdle : IMouseStates , IPointerClickHandler
    {
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButton(0))
        {
            RayCastMouse(mouseBehaviour);
            if (mouseBehaviour.selectedObject != null)
            {
                //Debug.Log(mouseBehaviour.selectedObject);
                return mouseBehaviour.mouseDragGear;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void RayCastMouse(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collidedObject = Physics2D.Raycast(positionOfMouse, Vector2.zero,float.PositiveInfinity,LayerData.MoveableGearLayer);
        RayCast();

        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
        //need to add code to get different things
        if (collidedObject.collider != null)
        {
            GetType(collidedObject.collider);
            Transform hitGameObject = collidedObject.collider.transform;
            mouseBehaviour.selectedObject = hitGameObject;
        }
    }

    private void RayCast()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        foreach(var raycastResult in raysastResults)
        {
            Debug.Log(raycastResult.gameObject.name);
        }
    }

    private System.Type[] existingSystemType = { typeof(Gear), typeof(JointBehaviour) }; 

    private void GetType(Collider2D collider)
    {
        foreach(System.Type type in existingSystemType)
        {
            if(collider.TryGetComponent(type, out Component compenent))
            {
                Debug.Log(compenent.gameObject.name);
                return;
            }
        }
        Debug.Log("no avaliable component detected");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
