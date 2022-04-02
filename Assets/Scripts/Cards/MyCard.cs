using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MyCard : MonoBehaviour
{
    #region ��������
    public int cardLevel = 1;
    public int maxLevel = 3;
    [HideInInspector]
    public virtual CardType cardType { get { return CardType.BASIC; } }
    [HideInInspector]
    public int position;
    [HideInInspector]
    public Dictionary<string, int> playInfo;    // ��¼һЩ�˺�֮��Ķ���
    [HideInInspector]
    public string description;
    public string cardName = "ʾ������";
    public string originalDescription = "ʾ�����Ƶ�����";
    public string tureDescription = "����������������";
    #endregion

    #region ��������
    public int damage;
    public int armor;

    // ����
    public int fire;
    public bool burn
    {
        get { return _burn; }
        set
        {
            _burn = value; 
            if (value)
            {
                OnCardBurn();
            } 
        }
    }
    private bool _burn = false;
    public int burnFactor = 2;

    // ����
    public int ice;
    public bool icebound = false;
    public int iceboundFactor = 1;

    #endregion

    #region ����
    private EventTrigger eventTrigger;
    #endregion

    #region Unity����
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        InitTriggers();
        EventCenter.AddListener(E_EventType.END_TURN, OnTurnEnd);
        playInfo = new Dictionary<string, int>();
        playInfo.Add("�˺�", 0);
        playInfo.Add("����", 0);
    }
    protected virtual void Start()
    {
        SetLevelData();
    }
    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.END_TURN, OnTurnEnd);
    }
    protected virtual void FixedUpdate()
    {
        UpdateDes();
    }
    #endregion

    #region UI�¼�
    protected virtual void UpdateDes()
    {

    }
    private void InitTriggers()
    {
        eventTrigger = GetComponent<EventTrigger>();
        AddPointerEvent(eventTrigger, EventTriggerType.PointerEnter, PointerEnter);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerExit, PointerExit);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerDown, PointerDown);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerUp, PointerUp);
        AddPointerEvent(eventTrigger, EventTriggerType.PointerClick, PointerClick);

    }
    protected virtual void PointerClick(BaseEventData arg0)
    {
        if (Input.GetKey(KeyCode.A))
        {
            PlayCard();
        }
        if (Input.GetKey(KeyCode.W))
        {
            DeckManager.Instance.AddCard(Instantiate(gameObject, GameObject.Find("Canvas").transform));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            DeleteSelf();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (cardLevel >= maxLevel)
            {
                cardLevel = 1;
            }
            else
            {
                cardLevel += 1;
            }
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (!burn && !icebound)
            {
                BurnCard();
                return;
            }
            else if (burn && !icebound)
            {
                IceboundCard();
                AddFire(10);
                return;
            }
            else if (burn || icebound)
            {
                burn = false;
                icebound = false;
                string _s = string.Format("��{0}���ָ�����", cardName);
                TipManager.ShowTip(_s);
                AddIce(10);
                return;
            }
        }
    }
    private void PointerUp(BaseEventData arg0)
    {
        MyCard _other;
        if (UIManager.Instance.ObjBePointed.TryGetComponent(out _other))
        {
            DeckManager.Instance.SwitchCard(_other.position, position);
        }
    }
    private void PointerDown(BaseEventData arg0)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            EventCenter.Broadcast(E_EventType.SHOW_ARROW);
        }
    }
    private void PointerExit(BaseEventData arg0)
    {
        UIManager.Instance.ObjBePointed = null;
    }
    private void PointerEnter(BaseEventData arg0)
    {
        UIManager.Instance.ObjBePointed = gameObject;
    }
    private void AddPointerEvent(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }
    #endregion

    #region ս��
    public virtual int CastDamage(int num)
    {
        if (burn)
        {
            num *= burnFactor;
        }
        int _damage = BattleManager.Instance.enemy.TakeDamage(num);
        if (_damage > 0)
        {
            OnCauseDamage();
            if (icebound)
            {
                GetArmor(_damage * iceboundFactor);
            }
        }
        playInfo["�˺�"] += _damage;
        return _damage;
    } 
    public virtual int DamageSelf(int num)
    {
        return BattleManager.Instance.player.TakeDamage(num);
    }
    public virtual void GetArmor(int armor)
    {
        BattleManager.Instance.player.armor += armor;
        playInfo["����"] += armor;
    }
    public virtual void OnCauseDamage()
    {
    }
    #endregion

    #region ս����ص�״̬�¼�
    protected virtual void BurnCard()
    {
        burn = true;
        icebound = false;
        string _s = string.Format("��{0}��ȼ����", cardName);
        TipManager.ShowTip(_s);
        AddFire(-10);
    }
    protected virtual void OnCardBurn()
    {

    }
    protected virtual void IceboundCard()
    {
        burn = false;
        icebound = true;
        string _s = string.Format("��{0}��������", cardName);
        TipManager.ShowTip(_s);
        AddIce(-10);
    }
    #endregion

    #region �������
    public virtual void SetLevelData()
    {
    }
    public virtual void PlayCard()
    {
        PreUse();
        OnUse();
        AfterUse();
        EventCenter.Broadcast<MyCard>(E_EventType.CARD_USED, this);
    }
    public virtual void TriggerCard()
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
        OnLose();
        ShowCardPlayInfo();
    }
    public virtual void OnGet()
    {
        
    }
    public virtual void DeleteSelf()
    {
        OnDelete();
        EventCenter.Broadcast(E_EventType.DELETE_CARD, position);
    }
    public virtual void OnDelete()
    {
        OnLose();
    }
    public virtual void OnLose()    // ��ɾ����������ᴥ��
    {

    }
    public virtual void ShowCardPlayInfo()
    {
        string _tmp = string.Format("��{0}��-��{1}���˺�����{2}������", cardName, playInfo["�˺�"], playInfo["����"]);
        TipManager.ShowTip(_tmp);
    }
    public virtual void OnTurnEnd()
    {
    }
    public virtual void OnTurnStart()
    {

    }
    #endregion

    #region ��ȡ����
    public int GetFire() { return BattleManager.Instance.player.fire; }
    public void SetFire(int num) { BattleManager.Instance.player.fire = num; }
    public void AddFire(int num) { BattleManager.Instance.player.fire += num; }
    public int GetIce() { return BattleManager.Instance.player.ice; }
    public void SetIce(int num) { BattleManager.Instance.player.ice = num; }
    public void AddIce(int num) { BattleManager.Instance.player.ice += num; }
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
    public MyCard GetCardByPos(int index)
    {
        if (IsIndexLegal(index))
        {
            return DeckManager.Instance.myCardInFlow[index].GetComponent<MyCard>();
        }
        else
        {
            return null;
        }
    }
    public MyCard GetLeftCard()
    {
        if (IsIndexLegal(position + 1))
        {
            return DeckManager.Instance.myCardInFlow[position + 1].GetComponent<MyCard>();
        }
        else
        {
            return null;
        }
    }
    public MyCard GetRightCard()
    {
        if (IsIndexLegal(position - 1))
        {
            return DeckManager.Instance.myCardInFlow[position - 1].GetComponent<MyCard>();
        }
        else
        {
            return null;
        }
    }
    public bool IsIndexLegal(int index)
    {
        int _flow_length = DeckManager.Instance.myCardInFlow.Count;
        if (index >= 0 && index < _flow_length)
        {
            return true;
        }
        return false;
    }
    public bool IsNextCardCombo()   // �Լ���������ǲ���һ����ɫ��
    {
        MyCard _card;
        if (DeckManager.Instance.myCardInFlow[position].TryGetComponent<MyCard>(out _card))
        {
            if (_card.cardType == cardType)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

}
