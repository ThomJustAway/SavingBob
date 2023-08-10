using System.Collections;
using UnityEngine;

public class LayerData 
{
    /*
     This script is to be used for getting layermask for different collision that 
    can happen in the game. It is a bit frustrating to keep on using magic strings 
    to get layermask so this is the solution I came up with

    This script is just storing different layermask in memory so that I can use it
      
     This is helpful as I dont have to keep on using magic strings
     to get what the layermask I want. I can just use this layerdata to
    get what data I want to. Anther thing is that I dont have to find 
    and change all the magic strings if there is an error to the string. 
    I can directly change it here to update all the script that uses the mask.
    
    //I will be going through this mask in this script
     */

    private static LayerMask entireAreaLayer = LayerMask.GetMask("GearAreaLayer");
    //EntireAreaLayer mask is to detect all avaliable gears and joint. As the name suggest, it gets all the entire area of a any gear/joint
    private static LayerMask innerAreaLayer = LayerMask.GetMask("InnerGearAreaLayer");
    //innerAreaLayer is a mask for a collider in walls/ gear inner area/ joints. This is used to tells certain script that the 
    //area is invalid and cant be used. You can look at the gear, joint prefabs to see this
    private static LayerMask moveableItemLayer = LayerMask.GetMask("MoveableGearLayer");
    //MoveableItemLayer is used to find Imoveable object in the game. all the IMoveable prefabs will use this layer in order for the mouse to detect them
    private static LayerMask jointLayer = LayerMask.GetMask("JointLayer");
    //JointLayer is a mask to help the game differenciate what is a joint and a gear.
    private static LayerMask uiLayer = LayerMask.GetMask("UI");
    //Legacy code but in case the game require to use the UIlayer mask

    public static LayerMask MoveableItemLayer { get { return moveableItemLayer; } }
    public static LayerMask InnerAreaLayer { get { return innerAreaLayer; } }
    
    public static LayerMask EntireAreaLayer { get { return entireAreaLayer; } }

    public static LayerMask JointLayer { get { return jointLayer; } }

    public static LayerMask UILayer { get { return uiLayer; } }
}
