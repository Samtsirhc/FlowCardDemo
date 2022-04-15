using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public GameObject ShouUnitStatus;
    virtual public int hp { get; set; }
    virtual public int armor { get; set; }
    virtual public int fire { get; set; }
    virtual public int ice { get; set; }

    private void UpdateUnitStatus()
    {
        string _s = "";
        if (hp > 0)
        {
            _s += "���� " + hp + "\n";
        }
        if (armor > 0)
        {
            _s += "���� " + armor + "\n";
        }
        if (fire > 0)
        {
            _s += "���� " + fire + "\n";
        }
        if (ice > 0)
        {
            _s += "���� " + ice + "\n";
        }
        ShouUnitStatus.GetComponent<Text>().text = _s;
    }
    protected virtual void Start()
    {
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateUnitStatus();
    }
    public int TakeDamage(int damage)
    {
        int _damage = 0;
        if (damage >= armor)
        {
            _damage = damage - armor;
            armor = 0;
            hp -= _damage;
        }
        else
        {
            armor -= damage;
        }
        return _damage;
    }

    /// <summary>
    /// ChuanCi ShangHai �����˺�
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int TakePiercingDamage(int damage){
        hp -= damage;
        return damage;
    }
    #region �������

    public virtual void OnTurnEnd()
    {
        armor = 0;
    }

    public virtual void OnTurnStart()
    {

    }
    #endregion
}
