using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData : MonoBehaviour
{
    public static ColorData Instance { get; private set; }
    public Color validPositionColor = Color.white;
    public Color invalidPositionColor = Color.white;
    public Color normalColor = Color.white;
    public Color StartingGearColor = Color.green;
    public Color EndingGearColor = Color.red;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else { Debug.LogWarning("Cannot have more then one color data"); }
    }
}
