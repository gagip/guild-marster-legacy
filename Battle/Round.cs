using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

/// <summary>
/// ������ ���� ���谡 �� ���� ûũ
/// </summary>
public class Round : MonoBehaviour
{

    public UnityAction RoundEndEvent;

    private readonly BattleManager battleManager;

    private readonly List<Adventurer> adventurers;
    private readonly List<Monster> monsters;

    public Round(List<Adventurer> adventurers, List<Monster> monsters)
    {
        battleManager = BattleManager.Instance;
        this.adventurers = adventurers;
        this.monsters = monsters;
    }

    public void StartRound()
    {
        if (adventurers == null || monsters == null) return;

        var entities = new List<Entity>(adventurers).Concat(monsters).ToList();
        entities.ForEach(x => x.SetStat());
        battleManager.AddLog("���͸� ������");
        while (!IsAllDie(adventurers) && !IsAllDie(monsters))
        {
            StartTurn();
        }
        battleManager.AddLog("������ ���̳���");
        battleManager.isSuccess = IsAllDie(monsters);
        RoundEndEvent.Invoke();
    }

    private bool IsAllDie<T>(List<T> entities) where T : Entity
    {
        return GetFirstLiveEntity(entities) == null;
    }

    public void StartTurn()
    {
        var entities = new List<Entity>(adventurers).Concat(monsters).ToList();
        entities = entities.OrderBy(x => x.Agi).ToList();

        foreach (Entity entity in entities)
        {
            // �׾ ���� ����
            if (entity.IsDie())
            {
                continue;
            }

            if (entity is Adventurer)
            {
                var monster = GetFirstLiveEntity(monsters);
                if (monster != null)
                {
                    Debug.Log(string.Format("{0}�� ü�� : {1}", monster.CharName, monster.Hp));
                    entity.Attack(monster);
                }
            }
            else if (entity is Monster)
            {
                var adventurer = GetFirstLiveEntity(adventurers);
                if (adventurer != null)
                {
                    Debug.Log(string.Format("{0}�� ü�� : {1}", adventurer.CharName, adventurer.Hp));
                    entity.Attack(adventurer);
                }
            }
        }
    }

    /// <summary>
    /// ��Ƽ �� ����ִ� �ն��� ����ü ��ȯ
    /// </summary>
    private Entity GetFirstLiveEntity<T>(List<T> entities) where T : Entity
    {
        try
        {
            return entities.Where(entity => !entity.IsDie()).First();
        }
        catch
        {
            return null;
        }
    }
}
