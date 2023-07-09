using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Archer : Entity
{

    protected override void AttackDistance()
    {
        attackDistance[0]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y)));
        attackDistance[1]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y)));
        attackDistance[2]=(new Vector2((tile.positionTile.x),(tile.positionTile.y+1)));
        attackDistance[3]=(new Vector2((tile.positionTile.x),(tile.positionTile.y-1)));
        attackDistance[4]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y+1)));
        attackDistance[5]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y-1)));
        attackDistance[6]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y)));
        attackDistance[7]=(new Vector2((tile.positionTile.x-2),(tile.positionTile.y+1)));
        attackDistance[8]=(new Vector2((tile.positionTile.x-2),(tile.positionTile.y+2)));
        attackDistance[9]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y+2)));
        attackDistance[10]=(new Vector2((tile.positionTile.x),(tile.positionTile.y+2)));
        attackDistance[11]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y+1)));
        attackDistance[12]=(new Vector2((tile.positionTile.x+2),(tile.positionTile.y)));
        attackDistance[13]=(new Vector2((tile.positionTile.x+2),(tile.positionTile.y-1)));
        attackDistance[14]=(new Vector2((tile.positionTile.x+2),(tile.positionTile.y-2)));
        attackDistance[15]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y-2)));
        attackDistance[16]=(new Vector2((tile.positionTile.x),(tile.positionTile.y-2)));
        attackDistance[17]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y+1)));
        attackDistance[18]=(new Vector2((tile.positionTile.x-2),(tile.positionTile.y)));
    }
    
}
