using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData : MonoBehaviour
{
    //will take the persisted color data and put it as a singleton in the game
    //so that gears can differentiate themselves
    //usually this is place in a gameobject so that the gears have a way to access this colors

    //This is not a good way since I am coupling color data and the gears together.
    //this can be seen in the level manager
    public static ColorData Instance { get; private set; }
    [SerializeField] private ColorDataScriptableObject scriptableObject;
    //will get the colors from a the color data scriptable object. This is important as 
    //the color can persist throughout different scene using the same color palette

    //all this colors here is the different color variation the gear can consist
    public Color ValidPositionColor { get; private set; } //this is for when the gear is being move
    public Color InvalidPositionColor { get; private set; } //to show players that the dragable gears is in a invalid position
    public Color NormalColor { get; private set; }// normal color for all the dragable gears
    public Color StartingGearColor { get; private set; }// color for all starting gears.
    public Color EndingGearColor { get; private set; }// color for all ending gear color

    public Color ObsticleGearColor { get; private set; }//color for all obsticle gear
    private void Awake()
    {// will check if there is only one instance of the script. if exist, destroy itself
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        //debugging purpose
        else { Debug.LogWarning("Cannot have more then one color data"); }
    }

    private void Init()
    {
        //set all the colors from the colordata scriptableobject
        ValidPositionColor = scriptableObject.validPositionColor;
        InvalidPositionColor = scriptableObject.invalidPositionColor;
        NormalColor = scriptableObject.normalColor;
        StartingGearColor = scriptableObject.StartingGearColor;
        EndingGearColor = scriptableObject.EndingGearColor;
        ObsticleGearColor = scriptableObject.ObsticleGearColor;
    }
}
