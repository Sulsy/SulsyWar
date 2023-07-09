using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class CombatSimulator
{
        public Entity winnerEntity;
        public Entity loseEntity;
        public GameController gameController;

    public CombatSimulator(GameController gameController)
    {
        this.gameController = gameController;
    }

    public bool? IsEntityLoseBattle(Entity playerEntity, Entity enemyEntity)
    {
        playerEntity.EntityDidAction();
        if (playerEntity.power > enemyEntity.power)
        {
            PlayerEntityWinnerBattle(playerEntity,enemyEntity);
            return true;
        }
        else if (playerEntity.power < enemyEntity.power)
        {
            PlayerEntityLoseBattle(playerEntity,enemyEntity);
            return false;
        }
        else
        {
            gameController.entities.Remove(playerEntity);
            gameController.entities.Remove(enemyEntity);
            playerEntity.EntityDestroy();
            enemyEntity.EntityDestroy();
            return null;
        }
    }

    private void PlayerEntityLoseBattle(Entity playerEntity,Entity enemyEntity)
    {
        winnerEntity = enemyEntity;
        loseEntity = playerEntity;
        winnerEntity.power -= loseEntity.power;
        if (IsEntityArcher(playerEntity)&&!IsEntityPowerZero(winnerEntity)) return;
        gameController.entities.Remove(playerEntity);
        playerEntity.EntityDestroy();
    }
    private void PlayerEntityWinnerBattle(Entity playerEntity,Entity enemyEntity)
    {
        loseEntity = enemyEntity;
        winnerEntity = playerEntity;
        loseEntity.power -= winnerEntity.power;
        if (IsEntityArcher(playerEntity)&&!IsEntityPowerZero(loseEntity)) return;
        gameController.entities.Remove(enemyEntity);
        enemyEntity.EntityDestroy();
    }
    public bool IsEntityArcher(Entity playerEntity)
    {
        return playerEntity.distanceDamage;
    }

    private bool IsEntityPowerZero(Entity entity)
    {
        return entity.power<=0;
    }
}
