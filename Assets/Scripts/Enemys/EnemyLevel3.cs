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
                player.TakeDamage(24);
                nextAct = "͵ȡ������еĻ���";
                break;
            case 2:
                armor = player.armor;
                player.armor = 0;
                nextAct = "����14";
                break;
            case 3:
                player.TakeDamage(14);
                nextAct = "����25 ʯ��1";
                break;
            case 4:
                player.TakeDamage(25);
                //ʯ��
                nextAct = "����10";
                break;
            case 5:
                armor += 10;
                nextAct = "͵ȡ������еĻ���";
                break;
            case 6:
                armor = player.armor;
                player.armor = 0;
                nextAct = "����20";
                break;
        }
        actTurn = actTurn % 6 + 1;
    }
}
