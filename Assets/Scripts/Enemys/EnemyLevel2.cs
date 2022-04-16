using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{

    private int actTurn = 1;
    public int Hp = 70;
    public Unit player;
    protected override void Start(){
        base.Start();
        hp = Hp;
        nextAct = "����14";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                player.TakeDamage(14);
                nextAct = "����12 ʯ��1";
                break;
            case 2:
                player.TakeDamage(12);
                //ʯ��
                nextAct = "����8";
                break;
            case 3:
                armor += 8;
                nextAct = "����18 ";
                break;
            case 4:
                player.TakeDamage(18);
                nextAct = "����20 ʯ��1";
                break;
            case 5:
                player.TakeDamage(20);
                //ʯ��
                nextAct = "����14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }
}
