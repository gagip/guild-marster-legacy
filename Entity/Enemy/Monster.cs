using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster: Entity
{
    public Monster()
    {
    }

    public Monster(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {
    }

    public int Exp { get; protected set; }
    public string Desc { get; protected set; }
    public string Type { get; protected set; }
    public string Item { get; protected set; }

    public float dropRate = 0.5f;

    public Item item;

    /// <summary>
    /// 아이템 드랍
    /// </summary>
    public Item DropItem()
    {
        float randomRate = Random.Range(0f, 1f);
        if (randomRate < dropRate)
        {
            return item;
        }
        return null;
    }



    public void SetEnemy(int idx)
    {
        // DB
        DBManager dBManager = DBManager.Instance;
        dBManager.OpenDB("Hunters.db");
        ArrayList data = dBManager.SelectById("Monster", idx, "name, lv, atk, hp, exp, text, type, item");

        // 데이터 가져오기
        //Name = (string)data[0];
        Lv = System.Convert.ToInt32(data[1]);
        Hp = System.Convert.ToInt32(data[3]);
        Exp = System.Convert.ToInt32(data[4]);
        Desc = (string)data[5];
        Type = (string)data[6];
        Item = (string)data[7];
    }
}
