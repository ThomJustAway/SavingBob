
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

    [SerializeField] private string nameOfElement; //setting this private to prevent accidentally changing the moveable element
    [SerializeField] private int cost;//same here

    private SpriteRenderer spriteRenderer; //will require to change the color of the gear to indicate movement
    public int Cost => cost; //this is implement the Imoveable interface
    public string Name => nameOfElement; 
    public GameObject Getprefab => gameObject;

    private ColorData colorData = ColorData.Instance;

    private Vector3 previousValidPosition; // this is important as it keeps memory of the previous valid position the gear is in before moving
    //if there is no valid position when move, this will be used in order to resolve the invalid position.
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void CheckValidPosition()
    {
        /* this will check if the gear is at a valid posititon 
            A valid position is define as a area where the
            Gear has no collision at its surrounding area.

            If there is valid position, it will remain at that position.
            However, if there is no valid position, it will try to do some 
            operation to figure out how resolve this issue.
         */

        //this is called after moving and draggin
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerAreaLayer);
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
            // if there is more than one around the gear, then give up resolving and go back to last position
            TryGoBackLastPosition();
        }
        else if (surroundingJoint.Length > 0)
        {
            // if there is no gear surrounding the area and the joint. 
            if (surroundingJoint.Length == 1)
            {
                //if it is only one, then just go to that joint position. (this will look like the gears locks to the joint)
                Collider2D joint = surroundingJoint[0];
                transform.position = joint.gameObject.transform.position;
            }
            else
            {
                //this means there is more than more joint.
                //It is harder to figure out what to do this so just return to last position
                TryGoBackLastPosition();
            }
        } //change this

        //do another check just in case the moving of valid position is compromise by another rotatable element
        surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerAreaLayer);

        if (surroundInnerGear.Length > 0)
        { 
            //that means that the moving of gear is still within a invalid spot
            TryGoBackLastPosition();
        }
        // at the end of all, the position of the gear is resolve. So return to normal color and play the placing sound
        spriteRenderer.color = colorData.NormalColor;
        MusicManager.Instance.PlayMusicClip(SoundData.PlacingSound);
    }
    public void Move(Vector3 position)
    {
        //this is called at the mousemove Behaviour and is called when the Dragable gear needs moving
        //it will take a specified amount position to move to (world position) and would move to that
        //area. 

        if (speed > 0)
        { //set and make sure the when moving, there is no rotation by the gear
            speed = 0;
        }
        transform.position = position;//change the position of the transform.position
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerAreaLayer);
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
        //this is to remove the game object.
        var itemButtons = LevelManager.instance.itemButtons;
        for (int i = 0; i < itemButtons.Length; i++) 
        /*
         What this does is just loop through all the item buttons so that it can 
        find the item button that is related to the dragable gear. Once found, it 
        will return to that specific item button pool
         */
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



