using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Colors color;
    public int playersId;
    public Entity entityMoved;
    public int money;
    public int reward;
    public int spent;

    private List<FacilityMetaData> _tileMetaDats;
    private List<FacilityMetaData> _entityDats;

    public void Initialization()
    {
        _tileMetaDats = new List<FacilityMetaData>();
        _entityDats = new List<FacilityMetaData>();
    }

    public void AddObjectMetaData<T>(T objectData)
    {
      
    }
    public void NewMove(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
           money+= entity.NewPlayerMove();
        }
    }
    

}
