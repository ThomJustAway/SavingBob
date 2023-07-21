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
    private bool IsHovered = false; //to check if the mouse is hovering the gear. The monobehviour on hover sometimes does not work
    private bool isActivated;
    private bool hasPlayedMusic;

    private void Awake()
    {
        gameObject.tag = "InactivedGear"; // this is not great idea to find gameobject ....
        hasPlayedMusic = false;
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

    }

    private void Update()
    {
        isActivated = IfConditionMet(gearHost.Speed);
        //when it is not activated
        if (hasPlayedMusic == false && isActivated == true)
        {
            MusicManager.Instance.PlayMusicClip(SoundData.EndGearRotating);
            hasPlayedMusic = true;
        }
        if(isActivated == false)
        {
            hasPlayedMusic = false;
        }
        CheckMouseHovering();
    }
    public bool IfConditionMet(float speed)
    {
        bool speedConditionMet = GetSpeedConditionIsMet(speed);
        return speedConditionMet;
    }

    // the naming conventiion here is confusing

    private bool GetSpeedConditionIsMet(float speed)
    {
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
        Vector3 mousePosition = Input.mousePosition;
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
        float distance = Vector2.Distance(mousePosition, gearPosition);
        return gearHost.GearRadius >= distance;
    }

    private string CreateMessage()
    {
        string message = $"Speed condition: {speedCondition}\n " +
                             $"Current Speed: {gearHost.Speed}\n ";
        return message;
    }
    //re look at this later
}



