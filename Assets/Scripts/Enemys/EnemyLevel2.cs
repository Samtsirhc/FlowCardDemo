using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{

    private int actTurn = 1;
    protected override void Start(){
        base.Start();
        nextAct = "����14";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                AttackPlayer(14);
                nextAct = "����12 ʯ��1";
                break;
            case 2:
                AttackPlayer(12);
                //ʯ��
                nextAct = "����8";
                break;
            case 3:
                GetArmor(8);
                nextAct = "����18 ";
                break;
            case 4:
                AttackPlayer(18);
                nextAct = "����20 ʯ��1";
                break;
            case 5:
                AttackPlayer(20);
                //ʯ��
                nextAct = "����14";
                break;
        }
        actTurn = actTurn % 5 + 1;
    }
}
