using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

/// <summary>
/// 퀘스트
/// </summary>
[System.Serializable]
public class AfterQuest : BeforeQuest
{
    public delegate void StartQuestEvent();
    public event StartQuestEvent startQuestEvent;

    [SerializeField] bool isRecon;

    [SerializeField] int pay;								// 보수

    [SerializeField] Adventurer[] party = new Adventurer[1];
    [SerializeField] Adventurer[] recommendAdventurer = new Adventurer[3];		// 추천 직업

    public AfterQuest() { }

    public AfterQuest(int idx) : base(idx)
    {
        questType = QuestType.NONE;
        SetRequest(idx);
    }

    public AfterQuest(BeforeQuest request)
    {
        this.Client = request.Client;
        this.Content = request.Content;
        this.Retainer = request.Retainer;
        this.Credit = request.Retainer;
        this.Lv = request.Lv;
        this.questType = request.questType;
        this.Title = request.Title;
    }

    /// <summary>
    /// 파티 참가
    /// </summary>
    /// <param name="adventurer"></param>
    /// <param name="idx"></param>
    public void JoinParty(int idx, Adventurer adventurer)
    {
        if (idx > party.Length || idx < 0) return;

        party.SetValue(adventurer, idx);
        adventurer.IsWorking = true;

        // 풀 파티일시 알림
        if (IsFullParty())
        {
            startQuestEvent();
            BattleManager.Instance. StartBattle(new List<Adventurer>(party));
        }
    }

    /// <summary>
    /// 파티 탈퇴
    /// </summary>
    /// <param name="idx"></param>
    public void DemitParty(int idx)
    {
        if (idx > party.Length || idx < 0) return;

        party.SetValue(null, idx);
    }

    /// <summary>
    /// 풀파티인가?
    /// </summary>
    public bool IsFullParty()
    {
        foreach (Adventurer adventurer in party)
        {
            if (adventurer == null)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 파티 남아 있는 자리 찾기
    /// </summary>
    /// <returns>-1은 자리가 없다는 뜻</returns>
    public int GetIdxEmptyParty()
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    #region property
    public bool IsRecon { get { return isRecon; } set { isRecon = value; } }       // 정찰 여부

    public int Pay
    {
        get { return pay; }
        set
        {
            if (value <= 0) pay = 0;
            else pay = value;
        }
    }

    public Adventurer[] Party
    {
        get { return party; }
        set { party = value; }
    }
    #endregion
}
