using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData
{
    /*
        Sound data is a class that purpose is to store this magic strings in memory
        this is espically useful as I dont have to keep on using magic string to tell
        the music manager to play a clip. I can just use this to store magic string so
        that I can use it when I require to play the music clip.

        This script is only meant for development and makes code much more readable and
        makes debugging more easy.
     */

    public static string PlacingSound { get { return "PlacingEffect"; } }
    public static string WinningSound { get { return "winning sound"; } }
    public static string EndGearRotating { get { return "EndGearRotating"; } }
    public static string SellingGear { get { return "SellingMoney"; } }
    public static string BuyingGear { get { return "BuyGearSound"; } }
    public static string ClickButton { get { return "clickButtonSound"; } }
    public static string NoMoneySoundEffect { get { return "NoMoneySoundEffect"; } }

    public static string ObsticleMoving { get { return "SlideObsticle"; } }
}
