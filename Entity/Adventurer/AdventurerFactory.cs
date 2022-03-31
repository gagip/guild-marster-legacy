using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerFactory
{

    public static Adventurer GetAdventurer()
    {
        Adventurer adventurer;
        AdventurerType type = (AdventurerType) Random.Range(0, 2);

        switch (type)
        {
            case AdventurerType.WARRIOR:
                adventurer = new Warrior();
                break;
            case AdventurerType.THIRF:
                adventurer = new Rogue();
                break;
            case AdventurerType.MAGICIAN:
                adventurer = new Mage();
                break;
            default:
                adventurer = new Warrior();
                break;
        }
        adventurer = GetDBHunter(adventurer);
        adventurer.CharName = GetDBHunterName();
        return adventurer;
    }

    private static string GetDBHunterName()
    {
        int idx = Random.Range(1, 30);

        // DB
        DBManager dBManager = DBManager.Instance;
        dBManager.OpenDB("Hunters.db");
        ArrayList data = dBManager.SelectById("HunterName", idx, "id, name");

        return (string)data[1];
    }

    private static Adventurer GetDBHunter(Adventurer adventurer)
    {
        int idx = Random.Range(0, 2);

        // DB
        DBManager dBManager = DBManager.Instance;
        dBManager.OpenDB("Hunters.db");
        ArrayList data = dBManager.SelectById("hunters", idx, "id, name, job, level, hp, str, con, int, spi, agi");

        adventurer.ClassName = (string)data[1];
        // adventurer.Lv = System.Convert.ToInt32(data[3]);
        adventurer.Lv = 1;
        adventurer.Hp = System.Convert.ToInt32(data[4]);
        // adventurer.Str = System.Convert.ToInt32(data[5]);
        // adventurer.Con = System.Convert.ToInt32(data[6]);
        // adventurer.Intel = System.Convert.ToInt32(data[7]);
        // adventurer.Spi = System.Convert.ToInt32(data[8]);
        // adventurer.Agi = System.Convert.ToInt32(data[9]);
        adventurer.Str = Random.Range(1, 11);
        adventurer.Con = Random.Range(1, 11);
        adventurer.Intel = Random.Range(1, 11);
        adventurer.Spi = Random.Range(1, 11);
        adventurer.Agi = Random.Range(1, 11);

        return adventurer;
    }
}
