using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public override int fire
    {
        get { return _fire; }
        set
        {
            if (canGetFire)
            {
                _fire = value;
                Debug.Log(value + "����");
            }
            else
            {
                if (value - _fire <= 0)
                {
                    _fire = value;
                }
            }
        }
    }
    [HideInInspector]
    public bool canGetFire = true;
    private int _fire = 0;

    protected override void Start()
    {
        base.Start();
        canGetFire = true;
        hp = 100;
    }

    protected override void UpdateUnitStatus() {
        base.UpdateUnitStatus();
        ShouUnitStatus.GetComponent<Text>().text += "���� " + BattleManager.Instance.playCost + "\n";
    }

    public override void OnTurnEnd(){
        armor = 0;
    }
}
