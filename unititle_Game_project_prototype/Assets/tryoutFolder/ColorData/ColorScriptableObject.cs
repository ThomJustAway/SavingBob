using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData")]
public class ColorScriptableObject : ScriptableObject
{
    public Color validPositionColor = Color.white;
    public Color invalidPositionColor = Color.white;
    public Color normalColor = Color.white;

}
