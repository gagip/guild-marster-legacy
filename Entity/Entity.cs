using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity
{
    [SerializeField] protected string charName;
    [SerializeField] protected int maxHp;               // �ִ� HP
    [SerializeField] protected int hp;                  // ���� HP
    [SerializeField] protected int lv;                  // ����
    [SerializeField] protected int exp;                 // ����ġ
    [SerializeField] protected int str;                 // ��
    [SerializeField] protected int con;                 // ü��
    [SerializeField] protected int intel;               // ����
    [SerializeField] protected int spi;                 // ����
    [SerializeField] protected int agi;                 // ��ø
    [SerializeField] protected int physDmg;             // �������ݷ�
    [SerializeField] protected int physDef;             // ��������
    [SerializeField] protected int mgcDmg;              // �������ݷ�
    [SerializeField] protected int mgcDef;              // ��������
    [SerializeField] protected float dodgeRate;         // ȸ����

    public event CallbackConfig.EntityDieCallback EntityDieEvent;

    public Entity()
    {
        EntityDieEvent += ShowDieText;
    }

    public Entity(string name, int lv, int str, int con, int intel, int spi, int agi) : this()
    {
        CharName = name;
        Lv = lv;
        Str = str;
        Con = con;
        Intel = intel;
        Spi = spi;
        Agi = agi;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetStat()
    {
        MaxHp = (int)(0.2f * Lv * Str + Con);
        Hp = MaxHp;
        PhysDmg = 1 + Str;
        PhysDef = Con;
        MgcDmg = Intel * 2;
        MgcDef = Mathf.RoundToInt(Spi * 1.5f);
        DodgeRate = (Agi / 5f) * (1/100);
    }

    public bool IsDie()
    {
        return Hp <= 0;
    }

    public void Attack(Entity entity)
    {
        int damage = 0;
        if (PhysDmg > MgcDmg)
        {
            damage = Mathf.Max(PhysDmg + (1 / 4) * MgcDmg, 1);
            entity.Defence(damage, true);
        }
        else
        {
            damage = Mathf.Max(MgcDmg + (1 / 4) * PhysDmg, 1);
            entity.Defence(damage, false);
        }
        BattleManager.Instance.AddLog(string.Format("{0}��(��) {1}��(��) ����!", this.CharName, entity.CharName));
    }

    /// <summary>
    /// �޴� ������ ���
    /// </summary>
    /// <param name="damage">���� ������ ��ġ</param>
    /// <param name="isPhys">�����������ΰ�?</param>
    public void Defence(int damage, bool isPhys)
    {
        // ȸ����
        float randomRate = Random.Range(0f, 1f);
        if (randomRate < DodgeRate)
        {
            BattleManager.Instance.AddLog(string.Format("{0}��(��) ȸ�ǿ� �����ߴ�!", this.CharName));
            return;
        }


        // ������ ����
        int realDamage;
        if (isPhys)
        {
            realDamage = Mathf.Max(damage * (PhysDef / (PhysDef + 100)), 1);
            Hp -= realDamage;
            BattleManager.Instance.AddLog(string.Format("{0}��(��) {1} �������� �޾Ҵ�.", this.CharName, realDamage));
        }
        else
        {
            realDamage = Mathf.Max(damage * (MgcDef / (MgcDef + 100)), 1);
            Hp -= realDamage;
            BattleManager.Instance.AddLog(string.Format("{0}��(��) {1} �������� �޾Ҵ�.", this.CharName, realDamage));
        }

        // ������ �̺�Ʈ �ߵ�
        if (IsDie())
        {
            EntityDieEvent.Invoke(this);
            BattleManager.Instance.AddLog(string.Format("{0}��(��) ��������", CharName));
            if (this is Monster)
            {
                Item dropItem = ((Monster)this).DropItem();
                if (dropItem != null)
                {
                    BattleManager.Instance.items.Add(dropItem);
                }
            }
        }
    }

    private void ShowDieText(Entity entity)
    {
        BattleManager.Instance.AddLog(string.Format("{0}��(��) ��������", CharName));
    }

    #region property
    public string CharName { get { return charName; } set { charName = value; } }
    public int Lv { get { return lv; } set { lv = value; } }
    public int Str { get { return str; } set { str = value; } }
    public int Con { get { return con; } set { con = value; } }
    public int Intel { get { return intel; } set { intel = value; } }
    public int Spi
    {
        get { return spi; }
        set { spi = value; }
    }
    public int Agi
    {
        get { return agi; }
        set { agi = value; }
    }
    public int Hp
    {
        get { return hp; }
        set
        {
            if (value < 0) hp = 0;
            else hp = value;
        }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            if (value < 0) maxHp = 0;
            else maxHp = value;
        }
    }
    public int PhysDmg { get { return physDmg; } protected set { physDmg = value; } }
    public int PhysDef { get { return physDef; } protected set { physDef = value; } }
    public int MgcDmg { get { return mgcDmg; } protected set { mgcDmg = value; } }
    public int MgcDef { get { return mgcDef; } protected set { mgcDef = value; } }
    public float DodgeRate { get { return dodgeRate; } protected set { dodgeRate = value; } }
    #endregion
}
