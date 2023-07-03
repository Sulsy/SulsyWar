using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Player> players;
    public Player activePlayer;

    private void Awake()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].MoveLimited += NextPlayer;
        }
    }

    public void NextPlayer()
    {
        if (activePlayer.playersId==0)
        {
            activePlayer = players[1];
        }
        else
        {
            activePlayer = players[0];
        }
        
    }
}
