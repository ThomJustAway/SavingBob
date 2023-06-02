using System.Collections;
using UnityEngine;

public class LayerData 
{
    private static LayerMask gearAreaLayer = LayerMask.GetMask("GearAreaLayer");
    private static LayerMask innerGearLayer = LayerMask.GetMask("InnerGearAreaLayer");
    private static LayerMask moveableGearLayer = LayerMask.GetMask("MoveableGearLayer");


    public static LayerMask MoveableGearLayer { get { return moveableGearLayer; } }
    public static LayerMask InnerGearLayer { get { return innerGearLayer; } }
    
    public static LayerMask GearAreaLayer { get { return gearAreaLayer; } }

}
