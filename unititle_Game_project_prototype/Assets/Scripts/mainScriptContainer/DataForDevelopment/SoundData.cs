using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData
{
    // this stores the magic string when calling out the clips. Much more reliable than using magic strings
    public static string PlacingSound { get { return "PlacingEffect"; } }
    public static string WinningSound { get { return "winning sound"; } }
    public static string EndGearRotating { get { return "EndGearRotating"; } }
    public static string SellingGear { get { return "SellingMoney"; } }
    public static string BuyingGear { get { return "BuyGearSound"; } }
    public static string ClickButton { get { return "clickButtonSound"; } }
    public static string NoMoneySoundEffect { get { return "NoMoneySoundEffect"; } }

    public static string ObsticleMoving { get { return "SlideObsticle"; } }
}
