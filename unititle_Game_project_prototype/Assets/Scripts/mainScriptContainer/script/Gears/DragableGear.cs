
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
        print(surroundInnerGear.Length);
        if (surroundInnerGear.Length == 1)
        {


            var gearComponent = surroundInnerGear[0].GetComponentInParent<Gear>();
            if(gearComponent != null)
            {
                Vector2 resolveDistance = CircleCalculator.DistanceToMoveBetweenDriverAndDrivenGear(gearComponent, this);
                transform.position = transform.position + (Vector3)resolveDistance;
            }
            else
            {// this mean that it is a wall
                print("it is a wall!");
                TryGoBackLastPosition();
            }
            //it appear that translate cant do this so I had to 
            //directly change the position

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
                Debug.LogWarning("Invalid position");
            }
        } //change this

        //surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);

        //if (surroundInnerGear.Length > 0)
        //{ //that means that the moving of gear is still within a invalid spot
        //    TryGoBackLastPosition() ;
        //}
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
        if (previousValidPosition != null)
        {
            transform.position = new Vector3(previousValidPosition.x,
                previousValidPosition.y,
                LayerManager.instance.GetGearZIndexBasedOnCurrentLayer());
        }
        //else
        //{
        //    //this means that the game object did not have a valid position and was just brought from thh gear menu
        //    DeleteGear();
        //}
    }

    private void DeleteGear()
    {
        var itemButtons = GameManager.instance.itemButtons;
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (itemButtons[i].IsGameObjectRelated(gameObject))
            {
                TooltipBehvaiour.instance.StartShortMessage("Remember to drag and drop!");
                itemButtons[i].RemoveItem(gameObject);
                break;
            }
        }
    }
}



