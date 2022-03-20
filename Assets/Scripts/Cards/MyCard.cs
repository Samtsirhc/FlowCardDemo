using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCard : MonoBehaviour
{
    public virtual CardType cardType { get { return CardType.BASIC; } }
    public int position;
    public int freezed
    {
        get { return _freezed; }
        set
        {
            _freezed = value;
            if (_freezed >= 1)
            {
                OnFreezed();
                if (coldAlarm)
                {
                    PlayCard();
                }
            }
        } 
    }
    private int _freezed = 0;
    public bool coldAlarm = false;


    [HideInInspector]
    public string description;
    public string cardName = "ʾ������";
    public string originalDescription = "ʾ�����Ƶ�����";
    public string tureDescription = "����������������";
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SwitchDescriptionType();
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
    }

    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
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
        if (Input.GetKey(KeyCode.F))
        {
            freezed += 1;
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
    public virtual void GetArmor(int armor)
    {
        BattleManager.Instance.player.armor += armor;
    }
    public virtual void OnCauseDamage()
    {

    }

    public virtual void AddCold(int cold)
    {
        BattleManager.Instance.enemy.cold += cold;
    }

    #region �������

    public virtual void OnFreezed()
    {

    }

    public virtual void PlayCard()
    {
        if (freezed >= 1 && !coldAlarm)
        {
            TipManager.ShowTip("�����Ʊ������ˣ�");
            return;
        }
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
        if (freezed >= 1)
        {
            freezed -= 1;
        }
    }

    public virtual void OnTurnStart()
    {

    }

    #endregion

    #region ��ȡ����
    public int GetAnger()
    {
        return BattleManager.Instance.player.anger;
    }
    public void SetAnger(int var)
    {
        BattleManager.Instance.player.anger = var;
    }
    public void AddAnger(int var)
    {
        BattleManager.Instance.player.anger += var;
    }
    public int GetCalm()
    {
        return BattleManager.Instance.player.calm;
    }
    public void SetCalm(int var)
    {
        BattleManager.Instance.player.calm = var;
    }
    public void AddCalm(int var)
    {
        BattleManager.Instance.player.calm += var;
    }
    public bool IsStartCard()
    {
        return BattleManager.Instance.maxPlayerCardTime - 1 == BattleManager.Instance.playCardTime;
    }
    public bool IsEndCard()
    {
        return 0 == BattleManager.Instance.playCardTime;
    }
    public int GetComboCount()
    {
        if (BattleManager.Instance.comboColor == cardType)
        {
            return BattleManager.Instance.comboCount;
        }
        else
        {
            return 0;
        }
    }
    #endregion
}
