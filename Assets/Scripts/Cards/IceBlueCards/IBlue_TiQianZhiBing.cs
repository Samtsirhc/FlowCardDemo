using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlue_TiQianZhiBing : IceBlueCard
{

    protected override void Start()
    {
        base.Start();
        switch (cardLevel)
        {
            case 1:
                armor = 4;
                break;
            case 2:
                armor = 6;
                break;
            case 3:
                armor = 8;
                break;
            default:
                break;
        }
    }

    public override void OnGet()
    {
        base.OnGet();
        GetArmor(armor);
    }

    protected override void UpdateDes()
    {
        base.UpdateDes();
        description = "";
        description += "��ã���û���" + armor + "; ";
    }
}
