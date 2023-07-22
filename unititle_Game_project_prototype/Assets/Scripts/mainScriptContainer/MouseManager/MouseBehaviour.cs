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

    //This behaviour uses the finite state machine pattern 

    //this is used for 
    /*
     1. moving gears
     2. deleting gear
     3. checking if the gear is placeable or not
     4. buying gears
     */
    [HideInInspector] public IMoveable selectedObject = null;
    [HideInInspector] public bool deleteActivated = false;
    [HideInInspector] public ItemButton[] itemButtons;

    //States
    [HideInInspector] public IMouseStates currentState;
    [HideInInspector] public MouseMoveSelectedObject mouseMoveSelectedObject = new MouseMoveSelectedObject();
    [HideInInspector] public MouseIdle mouseIdle = new MouseIdle();
    [HideInInspector] public DeleteItems deleteItems = new DeleteItems();

    private void Awake()
    {
        GameManager.instance.FinishCreatingGearButtonEvent += SetButton; ;
        GameManager.instance.SolvedEvent.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void SetButton(ItemButton[] obj)
    {
        itemButtons = obj;
        print(obj.Length);
        GameManager.instance.FinishCreatingGearButtonEvent -= SetButton;
    }

    private void Start()
    {
        currentState = mouseIdle;
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }
    public IMoveable GetImoveableComponent(Collider2D collider)
    {
        Transform hitGameObject = collider.transform;
        //mouseBehaviour.selectedObject = hitGameObject.GetComponent<IMoveable>();
        if (hitGameObject.TryGetComponent<IMoveable>(out IMoveable selectedObject))
        {
           return selectedObject;
        }
        else
        {
            //the joint
           return hitGameObject.GetComponentInParent<IMoveable>();
        }
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


