using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Gear))]
public class EndGearClass : MonoBehaviour
{
    private Gear gearHost;
    public Gear GearHost { get { return gearHost; } }
    [SerializeField] private float speedCondition;
    public bool IsActivated { get { return isActivated; } }
    
    //to check if the mouse is hovering the gear. The monobehviour on hover sometimes does not work with the current game
    private bool IsHovered = false;
    
    private bool isActivated;
    private bool hasPlayedMusic;

    private void Awake()
    {
        //set the game object as Inactivated gear so that gearremainderChecker can find the inactivated gear through tag
        gameObject.tag = "InactivedGear"; // this is not great idea to find gameobject due to perfromance issues
        hasPlayedMusic = false; //plays a music if it is activated, set to false to prevent it from playing the music
    }

    private void Start()
    {
        gearHost = GetComponent<Gear>();
        SetGearVisual();
    }

    private void SetGearVisual()
    {
        SpriteRenderer gearhostSpriteRenderer = gearHost.GetComponent<SpriteRenderer>();
        gearhostSpriteRenderer.color = ColorData.Instance.EndingGearColor;
        //change the color to show that it is a inactivated gear (end gear)
    }

    private void Update()
    {
        isActivated = GetSpeedConditionIsMet(gearHost.Speed);
        //when it is not activated
        if (hasPlayedMusic == false && isActivated == true)
        {
            MusicManager.Instance.PlayMusicClip(SoundData.EndGearRotating);
            hasPlayedMusic = true; //make sure the musicclip is played once
        }
        if(isActivated == false)
        {
            hasPlayedMusic = false;
        }
        CheckMouseHovering();
    }
    private bool GetSpeedConditionIsMet(float speed)
    {
        //if the current speed of the gear host is more than speed condition, then it is activated
        if (speed > speedCondition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckMouseHovering()
    {
        //will calculate the mouse position and figure out if that position is around the circle
        Vector3 mousePosition = Input.mousePosition;
        
        //get z index  so that it wont trigger with other inactivated gears at different layers
        mousePosition.z = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer();
        
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
        if (CheckIfWithinCircle(position))
        {
            if (TooltipBehvaiour.instance.IsActivated)
            {
                TooltipBehvaiour.instance.SetText(CreateMessage()); //changing the message on the file
            }
            else
            {
                TooltipBehvaiour.instance.StartMessage(CreateMessage());
                IsHovered = true; // made sure to check if the gear is hovered
            }
        }
        else
        {
            if (IsHovered)
            { //if hovered then delete the message
                TooltipBehvaiour.instance.EndMessage();
                IsHovered = false;
            }

        }
    }

    private bool CheckIfWithinCircle(Vector3 positionOfMouse)
    {
        if (transform.position.z != LayerManager.instance.GetGearZIndexBasedOnCurrentLayer())
        {//if the mosue is not at the layer of the end gear
            return false;
        }
        Vector2 mousePosition = new Vector2(positionOfMouse.x, positionOfMouse.y);
        Vector2 gearPosition = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(mousePosition, gearPosition); //calculate the distance the gear and the mouse
        return gearHost.GearRadius >= distance; //make sure it is within the gear radius
    }

    private string CreateMessage()
    {
        string message = $"Speed condition: {speedCondition}\n " +
                             $"Current Speed: {gearHost.Speed}\n ";
        return message;
    }
    //re look at this later
}



