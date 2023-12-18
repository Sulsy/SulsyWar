using System.Collections;
using System.Collections.Generic;
using Core;
using GoldenRations;
using UnityEngine;

public class Combatant : Entity
{
    public bool DistanceDamage { get; set; }
    public bool DidAction { get; private set; }
    public float Power { get; set; }
    
    public List<Vector2> moveDistance;
    public List<Vector2> attackDistance;

    public override void Initialization(EntityData data, Vector2 position, Vector3 transformPosition, Colors colors, int id)
    {
        Power = Random.Range(1, 100);
        for (int i = 0; i < 20; i++)
        {
            moveDistance.Add(new Vector2());
            attackDistance.Add(new Vector2());
        }
        base.Initialization(data,position,transformPosition,colors,id);
        MoveDistance();
    }

    public void Move(Vector3 transformPosition)
    {
        transform.position = new Vector3(transformPosition.x, transformPosition.y, -11);
        DidAction = true;
        MoveDistance();
    }

    public override int NewPlayerMove()
    {
        DidAction = false;
        return base.NewPlayerMove();
    }

    protected virtual void AttackDistance(){}

    protected virtual void MoveDistance(){}
}