using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : Warrior
{
    public override void DoPassiveSkill()
    {
        // 물리공격력 + 15%
        str += (int) (str * .15);
    }
}
