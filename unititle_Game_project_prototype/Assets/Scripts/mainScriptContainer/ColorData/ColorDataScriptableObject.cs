using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData")]

public class ColorDataScriptableObject : ScriptableObject
{
    //will store the colors and will persist throughout different levels
    //this is so that I dont have to change manually change the color if I want to
    //this is also more dynamic as I can have more than one script with different
    //color palette. This makes it easier to change the color of the gears.
    public Color validPositionColor = Color.white;
    public Color invalidPositionColor = Color.white;
    public Color normalColor = Color.white;
    public Color StartingGearColor = Color.green;
    public Color EndingGearColor = Color.red;
    public Color ObsticleGearColor = Color.green;
}
