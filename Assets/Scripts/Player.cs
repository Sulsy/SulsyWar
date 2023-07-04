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

    public void NewMove(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.NewPlayerMove();
        }
    }
    

}
