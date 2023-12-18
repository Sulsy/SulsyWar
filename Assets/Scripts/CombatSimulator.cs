using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class CombatSimulator
{
        public Combatant winnerEntity;
        public Combatant loseEntity;
        public GameController gameController;

    public CombatSimulator(GameController gameController)
    {
        this.gameController = gameController;
    }

     public bool? IsEntityLoseBattle(Combatant playerEntity, Combatant enemyEntity)
    {
       
        if (playerEntity.Power > enemyEntity.Power)
        {
            PlayerEntityWinnerBattle(playerEntity,enemyEntity);
            return true;
        }
        else if (playerEntity.Power < enemyEntity.Power)
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

    private void PlayerEntityLoseBattle(Combatant playerEntity,Combatant enemyEntity)
    {
        winnerEntity = enemyEntity;
        loseEntity = playerEntity;
        winnerEntity.Power -= loseEntity.Power;
        if (IsEntityArcher(playerEntity)&&!IsEntityPowerZero(winnerEntity)) return;
        gameController.entities.Remove(playerEntity);
        playerEntity.EntityDestroy();
    }
    private void PlayerEntityWinnerBattle(Combatant playerEntity,Combatant enemyEntity)
    {
        loseEntity = enemyEntity;
        winnerEntity = playerEntity;
        loseEntity.Power -= winnerEntity.Power;
        if (IsEntityArcher(playerEntity)&&!IsEntityPowerZero(loseEntity)) return;
        gameController.entities.Remove(enemyEntity);
        enemyEntity.EntityDestroy();
    }
    public bool IsEntityArcher(Combatant playerEntity)
    {
        return playerEntity.DistanceDamage;
    }

    private bool IsEntityPowerZero(Combatant entity)
    {
        return entity.Power<=0;
    }
}
