using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvantGarde : Warrior
{
    public AvantGarde(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {
    }

    public override void DoPassiveSkill()
    {
        // HP + 22.5%
        hp += (int) (hp * 0.225);
    }
}
