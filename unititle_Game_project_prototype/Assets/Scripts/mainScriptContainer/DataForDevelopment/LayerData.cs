using System.Collections;
using UnityEngine;

public class LayerData 
{
    // this is just data to keep the different layermask
    // very useful to change things on the fly
    private static LayerMask gearAreaLayer = LayerMask.GetMask("GearAreaLayer");
    private static LayerMask innerGearLayer = LayerMask.GetMask("InnerGearAreaLayer");
    private static LayerMask moveableGearLayer = LayerMask.GetMask("MoveableGearLayer");
    private static LayerMask jointLayer = LayerMask.GetMask("JointLayer");
    private static LayerMask uiLayer = LayerMask.GetMask("UI");

    public static LayerMask MoveableGearLayer { get { return moveableGearLayer; } }
    public static LayerMask InnerGearLayer { get { return innerGearLayer; } }
    
    public static LayerMask GearAreaLayer { get { return gearAreaLayer; } }

    public static LayerMask JointLayer { get { return jointLayer; } }

    public static LayerMask UILayer { get { return uiLayer; } }
}
