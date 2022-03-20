using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCard : MonoBehaviour
{
    public virtual CardType cardType { get { return CardType.BASIC; } }
    public int position;
    [HideInInspector]
    public string description;

    public string cardName = "ʾ������";
    public string originalDescription = "ʾ�����Ƶ�����";
    public string tureDescription = "����������������";
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SwitchDescriptionType();
    }


    private void FixedUpdate()
    {
        SwitchDescriptionType();
    }
    public void OnCardClicked()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PlayCard();
        }
        if (Input.GetKey(KeyCode.D))
        {
            EventCenter.Broadcast(E_EventType.DELETE_CARD, position);
        }
    }
    public void SwitchDescriptionType()
    {
        if (DeckManager.Instance.descriptionType)
        {
            description = originalDescription;
        }
        else
        {
            description = tureDescription;
        }
    }

    public virtual int CastDamage(int damage)
    {
        int _damage = BattleManager.Instance.enemy.TakeDamage(damage);
        if (_damage > 0)
        {
            OnCauseDamage();
        }
        return _damage;
    } 
    public virtual void OnCauseDamage()
    {

    }

    #region �������

    public virtual void PlayCard()
    {
        PreUse();
        OnUse();
        AfterUse();
    }

    public virtual void PreUse()
    {

    }
    public virtual void OnUse()
    {

    }
    public virtual void AfterUse()
    {
        EventCenter.Broadcast<MyCard>(E_EventType.CARD_USED, this);
    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnCombo()
    {

    }
    #endregion

    #region ��ȡ����
    public int GetAnger()
    {
        return BattleManager.Instance.player.anger;
    }
    #endregion
}
