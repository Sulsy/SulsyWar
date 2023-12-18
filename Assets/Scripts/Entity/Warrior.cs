using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Warrior : Combatant
{
    public override void Initialization(EntityData data, Vector2 position, Vector3 transformPosition, Colors colors, int id)
    {
        DistanceDamage = false;
        base.Initialization(data, position,transformPosition, colors, id);
    }
    protected override void AttackDistance()
    {
        attackDistance[0] = (new Vector2((Position.x + 1), (Position.y)));
        attackDistance[1] = (new Vector2((Position.x - 1), (Position.y)));
        attackDistance[2] = (new Vector2((Position.x), (Position.y + 1)));
        attackDistance[3] = (new Vector2((Position.x), (Position.y - 1)));
        attackDistance[4] = (new Vector2((Position.x - 1), (Position.y + 1)));
        attackDistance[5] = (new Vector2((Position.x + 1), (Position.y - 1)));
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
