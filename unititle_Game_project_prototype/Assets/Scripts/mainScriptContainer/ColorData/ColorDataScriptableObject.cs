﻿using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData")]

public class ColorDataScriptableObject : ScriptableObject
{
    //will store the colors and will persist throughout different levels
    public Color validPositionColor = Color.white;
    public Color invalidPositionColor = Color.white;
    public Color normalColor = Color.white;
    public Color StartingGearColor = Color.green;
    public Color EndingGearColor = Color.red;
}