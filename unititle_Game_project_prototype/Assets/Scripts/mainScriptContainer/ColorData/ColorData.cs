using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorData : MonoBehaviour
{
    //will take the persisted color data and put it as a singleton in the game
    //so that gears can differentiate themselves

    //This is not a good way since I am coupling color data and the gears together.
    public static ColorData Instance { get; private set; }
    [SerializeField] private ColorDataScriptableObject scriptableObject;
    public Color ValidPositionColor { get; private set; }
    public Color InvalidPositionColor { get; private set; }
    public Color NormalColor { get; private set; }
    public Color StartingGearColor { get; private set; }
    public Color EndingGearColor { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else { Debug.LogWarning("Cannot have more then one color data"); }
    }

    private void Init()
    {
        ValidPositionColor = scriptableObject.validPositionColor;
        InvalidPositionColor = scriptableObject.invalidPositionColor;
        NormalColor = scriptableObject.normalColor;
        StartingGearColor = scriptableObject.StartingGearColor;
        EndingGearColor = scriptableObject.EndingGearColor;

    }
}
