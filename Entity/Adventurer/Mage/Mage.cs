using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Adventurer
{
    public Mage()
    {
    }

    public Mage(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        hp += 2;
    }

    public override void DoPassiveSkill()
    {
        
    }
}
