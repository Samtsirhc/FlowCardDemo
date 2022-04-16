using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : Enemy
{
    private int actTurn = 1;
    public int Hp = 90;
    public Unit player;
    protected override void Start(){
        base.Start();
        hp = Hp;
        nextAct = "����20";
    }
    protected override void EnemyAct()
    {
        base.EnemyAct();
        switch(actTurn){
            case 1:
                AttackPlayer(24);
                nextAct = "͵ȡ������еĻ���";
                break;
            case 2:
                StealAllArmor();
                nextAct = "����14";
                break;
            case 3:
                AttackPlayer(14);
                nextAct = "����25 ʯ��1";
                break;
            case 4:
                AttackPlayer(25);
                //ʯ��
                nextAct = "����10";
                break;
            case 5:
                GetArmor(10);
                nextAct = "͵ȡ������еĻ���";
                break;
            case 6:
                StealAllArmor();
                nextAct = "����20";
                break;
        }
        actTurn = actTurn % 6 + 1;
    }
}
