using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Archer : Combatant
{
    public override void Initialization(EntityData data, Vector2 position, Vector3 transformPosition, Colors colors, int id)
    {
        DistanceDamage = true;
        base.Initialization(data, position, transformPosition,colors, id);
    }

    protected override void AttackDistance()
    {
        attackDistance[0]=(new Vector2((Position.x+1),(Position.y)));
        attackDistance[1]=(new Vector2((Position.x-1),(Position.y)));
        attackDistance[2]=(new Vector2((Position.x),(Position.y+1)));
        attackDistance[3]=(new Vector2((Position.x),(Position.y-1)));
        attackDistance[4]=(new Vector2((Position.x-1),(Position.y+1)));
        attackDistance[5]=(new Vector2((Position.x+1),(Position.y-1)));
        attackDistance[6]=(new Vector2((Position.x+1),(Position.y)));
        attackDistance[7]=(new Vector2((Position.x-2),(Position.y+1)));
        attackDistance[8]=(new Vector2((Position.x-2),(Position.y+2)));
        attackDistance[9]=(new Vector2((Position.x-1),(Position.y+2)));
        attackDistance[10]=(new Vector2((Position.x),(Position.y+2)));
        attackDistance[11]=(new Vector2((Position.x+1),(Position.y+1)));
        attackDistance[12]=(new Vector2((Position.x+2),(Position.y)));
        attackDistance[13]=(new Vector2((Position.x+2),(Position.y-1)));
        attackDistance[14]=(new Vector2((Position.x+2),(Position.y-2)));
        attackDistance[15]=(new Vector2((Position.x+1),(Position.y-2)));
        attackDistance[16]=(new Vector2((Position.x),(Position.y-2)));
        attackDistance[17]=(new Vector2((Position.x-1),(Position.y+1)));
        attackDistance[18]=(new Vector2((Position.x-2),(Position.y)));
    }

    protected override void MoveDistance()
    {
        moveDistance[0] = (new Vector2((Position.x + 1), (Position.y)));
        moveDistance[1] = (new Vector2((Position.x - 1), (Position.y)));
        moveDistance[2] = (new Vector2((Position.x), (Position.y + 1)));
        moveDistance[3] = (new Vector2((Position.x), (Position.y - 1)));
        moveDistance[4] = (new Vector2((Position.x - 1), (Position.y + 1)));
        moveDistance[5] = (new Vector2((Position.x + 1), (Position.y - 1)));
        AttackDistance();
    }
    
}
