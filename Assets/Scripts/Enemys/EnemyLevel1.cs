using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : Enemy
{
    private int actTurn = 1;
    public Unit player;
    protected override void Start(){
        base.Start();
        nextAct = "����14";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                player.TakeDamage(14);
                nextAct = "����8";
                break;
            case 2:
                armor += 8;
                nextAct = "����12";
                break;
            case 3:
                player.TakeDamage(12);
                nextAct = "����16";
                break;
            case 4:
                player.TakeDamage(16);
                nextAct = "����12";
                break;
            case 5:
                armor += 12;
                nextAct = "����14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }
}
