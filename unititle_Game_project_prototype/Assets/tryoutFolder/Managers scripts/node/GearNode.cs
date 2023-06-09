using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GearNode 
{
    private Gear mainGear;
    private TypeOfGearConnected[] gearsConnected;

    public Gear MainGear { get { return mainGear; } }
    public TypeOfGearConnected[] GearsConnected { get { return gearsConnected; } }

    public GearNode(Gear mainGear, TypeOfGearConnected[] gearsConnected)
    {
        this.mainGear = mainGear;
        this.gearsConnected = gearsConnected;
    }
}

public class TypeOfGearConnected
{
    private Gear gear;
    private bool isJointed;

    public Gear Gear { get { return gear; } }
    public bool IsJointed { get { return isJointed; } }

    public TypeOfGearConnected(Gear gear , bool isJointed)
    {
        this.gear = gear;
        this.isJointed = isJointed;
    }
}
