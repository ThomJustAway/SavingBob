
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
        //this is called after moving and draggin
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        Collider2D[] surroundingJoint = GetColliderAroundRadiusBasedOnLayer(LayerData.JointLayer);
        if (surroundInnerGear.Length == 1)
        {
            // as of now, it is only possible to correct position 
            //with only one gear rest of the time it will be invalid.
            var gearComponent = surroundInnerGear[0].GetComponentInParent<Gear>();
            if (gearComponent != null)
            {
                //calculate the distance using custom made distance calculator
                Vector2 resolveDistance = CircleCalculator.DistanceToMoveBetweenDriverAndDrivenGear(gearComponent, this);
                transform.position = transform.position + (Vector3)resolveDistance;
                /*
                add the distance to transform.position
                it appears that transform.resolve cant do this simple operation so :(    
                 */
            }
            else
            {// this mean that it is a wall as there is no gear component
             //fun fact: the walls use innergearlayer so that the dragable element can detect
             //if the dragable gear cant collect and Gear component, it means it has to be a wall
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

        //do another check just in case the moving of valid position is compromise by another rotatable element
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
        //this is called at the mousemove Behaviour and is called when the Dragable gear needs moving
        if (speed > 0)
        { //set and make sure the when moving, there is no rotation by the gear
            speed = 0;
        }
        transform.position = position;//change the position of the transform.position
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if (surroundInnerGear.Length > 0)
        {//if there is a inner gear, then tell the player that is cant be place there by changing the color
            spriteRenderer.color = colorData.InvalidPositionColor;
        }
        else
        {
            //change the color to green if there is no gear surrounding the gear
            spriteRenderer.color = colorData.ValidPositionColor;
            previousValidPosition = position;
        }
        //do some code to visually show that the gear can be place or not.
    }

    private void TryGoBackLastPosition()
    {
        //this will be called to return back to original position as it cannot find a suitable area
        // vector3.zero is because that is vector 3 struct initalise value
        if (previousValidPosition != Vector3.zero) // if have previous valid position
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
            //go through the item buttons and find the item buttons that correspond to the current dragable element
            if (itemButtons[i].IsGameObjectRelated(gameObject))
            {
                itemButtons[i].RemoveItem(gameObject);
                break;
            }
        }
    }
}



