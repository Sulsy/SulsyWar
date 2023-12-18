using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityMetaData
{
    public Vector2 Position { get; set; }
    public int EconomicValue{ get; set; }

    public FacilityMetaData(Vector2 position,int economicValue)
    {
        Position = position;
        EconomicValue = economicValue;
    }
}
