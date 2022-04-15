using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FRed_HuoYanChongJi : FireRedCard
{
    public int maxFireCost; //����������
    public int nowFire;
    public override void SetLevelData()
    {
        base.SetLevelData();
        switch(cardLevel){
            case 1:
                maxFireCost = 10;
                break;
            case 2:
                maxFireCost = 15;
                break;
            case 3:
                maxFireCost = 22;
                break;
            default:
                break;
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        nowFire = GetFire();
        damage = nowFire >= maxFireCost ? maxFireCost : nowFire; 
        CastDamage(damage);
        AddFire(-1 * damage);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        nowFire = GetFire();
        damage = nowFire >= maxFireCost ? maxFireCost : nowFire; 
        description = "";
        description += "����"+ damage + "�����,���" + damage +"�˺�\n";
        description += "��������"+ maxFireCost +"����档";
    }


}
