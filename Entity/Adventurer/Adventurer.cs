using UnityEngine;

[System.Serializable]
public abstract class Adventurer : Entity
{
    [SerializeField] protected string className;
    [SerializeField] protected bool isWorking;
    [SerializeField] protected TearType tearType = TearType.BRONZE;
    [SerializeField] protected ClassType classType;

    public Adventurer() : base()
    {
    }

    public Adventurer(string name, int lv, int str, int con, int intel, int spi, int agi) : base(name, lv, str, con, intel, spi, agi)
    {
    }

    protected virtual void LevelUp()
    {
        lv += 1;
    }

    public abstract void DoPassiveSkill();

    public string ClassName { get { return className; } set { className = value; } }
    public bool IsWorking { get { return isWorking; } set { isWorking = value; } }
    public TearType Tear { get { return tearType; } }
}
