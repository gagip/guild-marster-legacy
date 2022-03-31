using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Adventurer
{
    public Rogue() : base()
    {
    }

    public Rogue(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        hp += 3;
    }

    public override void DoPassiveSkill()
    {
        
    }
}
