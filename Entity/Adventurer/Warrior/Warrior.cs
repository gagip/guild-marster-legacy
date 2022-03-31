using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Adventurer
{
    public Warrior() : base()
    {

    }

    public Warrior(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {}

    protected override void LevelUp()
    {
        base.LevelUp();
        hp += 4;
    }

    public override void DoPassiveSkill()
    {
        // HP + 15%
        hp += (int) (hp * 0.15);
    }
}
