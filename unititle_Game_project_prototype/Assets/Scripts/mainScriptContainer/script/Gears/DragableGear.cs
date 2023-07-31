
using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.mainScriptContainer;

//Have a Gear class called Gear
//The dragable one are called dragable gears
// The 

public class DragableGear : Gear, IMoveable
{
    //readme
    /*
    Inherit the IMoveable which is a interface IMoveable which is used for all the dragging of gears.
    It uses the circle calculator to do the detection correction of placement. You can find out more
    under the Move and CheckValidPostion function
    */

    [SerializeField] private string nameOfElement;
    [SerializeField] private int cost;

    private SpriteRenderer spriteRenderer;
    public int Cost => cost;
    public string Name => nameOfElement;
    public GameObject Getprefab => gameObject;

    private ColorData colorData = ColorData.Instance;

    private Vector3 previousValidPosition;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void CheckValidPosition()
    {
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        Collider2D[] surroundingJoint = GetColliderAroundRadiusBasedOnLayer(LayerData.JointLayer);
        if (surroundInnerGear.Length == 1)
        {
            // as of now, it is only possible to correct position 
            //with only one gear rest of the time it will be invalid.
            var gearComponent = surroundInnerGear[0].GetComponentInParent<Gear>();
            if (gearComponent != null)
            {
                Vector2 resolveDistance = CircleCalculator.DistanceToMoveBetweenDriverAndDrivenGear(gearComponent, this);
                transform.position = transform.position + (Vector3)resolveDistance;
            }
            else
            {// this mean that it is a wall
                TryGoBackLastPosition();
            }
        }
        else if(surroundInnerGear.Length > 1)
        {
            TryGoBackLastPosition();
        }
        else if (surroundingJoint.Length > 0)
        {
            if (surroundingJoint.Length == 1)
            {
                Collider2D joint = surroundingJoint[0];
                transform.position = joint.gameObject.transform.position;
            }
            else
            {
                TryGoBackLastPosition();
            }
        } //change this

        surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);

        if (surroundInnerGear.Length > 0)
        { 
            //that means that the moving of gear is still within a invalid spot
            TryGoBackLastPosition();
        }
        spriteRenderer.color = colorData.NormalColor;
        MusicManager.Instance.PlayMusicClip(SoundData.PlacingSound);
    }
    public void Move(Vector3 position)
    {
        if (speed > 0)
        {
            speed = 0;
        }
        transform.position = position;
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if (surroundInnerGear == null)
        {
            return;
        }
        if (surroundInnerGear.Length > 0)
        {
            spriteRenderer.color = colorData.InvalidPositionColor;
        }
        else
        {
            spriteRenderer.color = colorData.ValidPositionColor;
            previousValidPosition = position;
        }
        //do some code to visually show that the gear can be place or not.
    }

    private void TryGoBackLastPosition()
    {
        if (previousValidPosition != Vector3.zero) 
            //i am not sure why but whenever a vector3 is initalise, the inital value is vector.zero
        {
            transform.position = new Vector3(previousValidPosition.x,
                previousValidPosition.y,
                LayerManager.instance.GetGearZIndexBasedOnCurrentLayer());
        }
        else
        {
            //this means that the game object did not have a valid position and was just brought from the gear menu
            TooltipBehvaiour.instance.StartShortMessage("Remember to drag and drop!");
            RemoveItem();
        }
    }

    public void RemoveItem()
    {
        var itemButtons = LevelManager.instance.itemButtons;
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (itemButtons[i].IsGameObjectRelated(gameObject))
            {
                itemButtons[i].RemoveItem(gameObject);
                break;
            }
        }
    }
}



