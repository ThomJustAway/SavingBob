using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBehaviour : MonoBehaviour
{
    //readme
    /*
    The mousebehaviour is what makes the mouse able to interact with the different
    items in the game such as dragging, deleting and buying gears.

    It uses the finite state machine pattern where the are a different states in use.

    The only bad thing I can comment is that many of the variables are public
    but as long as I dont expose this behaviour to other class other than the states, 
    the implementation should be fine
    */

    
    [HideInInspector] public IMoveable selectedObject = null; //You can see this used in the idle and mousemoveSelectedObject state
    [HideInInspector] public bool deleteActivated = false; // used only on the delete state. This is toggled by a delete button

    //States
    [HideInInspector] public IMouseStates currentState;
    [HideInInspector] public MouseMoveSelectedObject mouseMoveSelectedObject = new MouseMoveSelectedObject();
    [HideInInspector] public MouseIdle mouseIdle = new MouseIdle();
    [HideInInspector] public DeleteItems deleteItems = new DeleteItems();

    private void Awake()
    {
        LevelManager.instance.SolvedEvent.AddListener(() =>
        {
            //making sure that players cant interact with the game once the game is over
            gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        currentState = mouseIdle;
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }


    //this function is so for the states to use. Because I find it efficient to have the code share so if I wanted to change something, 
    //the rest of the states will change as well
    public IMoveable GetImoveableComponent(Collider2D collider)
    {
        //this function simplify the process getting the Imoveable component from both normal and edge cases.


        Transform hitGameObject = collider.transform;
        //mouseBehaviour.selectedObject = hitGameObject.GetComponent<IMoveable>();
        if (hitGameObject.TryGetComponent<IMoveable>(out IMoveable selectedObject))
        {
            //sometimes, the IMoveable component is together with collider component
           return selectedObject;
        }
        else
        {
            //the joint (which has the IMoveable component at the parent component)
           return hitGameObject.GetComponentInParent<IMoveable>();
        }
        //either way, it is easier to know I just getting the Imoveable component from the rotatable element
    }

    public void ToggleDeletedActiavted()
    {
        deleteActivated = !deleteActivated;
        if(deleteActivated)
        {
            TooltipBehvaiour.instance.StartMessage("\n<color=yellow>You are now on delete mode! \r\nTo stop, press the button again");
        }
        else
        {
            TooltipBehvaiour.instance.EndMessage();
        }
        //this is to show that the player is on delete mode.
    }

    public Vector3 GetVector3FromMousePosition()
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3Int cellPosition = mouseBehaviour.grid.WorldToCell(positionOfMouse);
        //make the gear follow the mouse while snaping to the grid
        //Vector3 newPosition = mouseBehaviour.grid.GetCellCenterLocal(cellPosition);
        var newPosition = new Vector3(positionOfMouse.x, positionOfMouse.y, LayerManager.instance.GetGearZIndexBasedOnCurrentLayer());
        // make sure that the gear is place on top of the UI component
        //newPosition.z = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer() ;
        return newPosition;
    }

}


