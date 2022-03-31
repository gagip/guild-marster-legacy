using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyManager : SingletonBase<ApplyManager>
{
    List<QuestBoard> questBoards;
    List<Adventurer> employees;

    [Tooltip("지원 주기 시간 (단위: 초)")] [SerializeField] int applyTime;

    private void Start()
    {
        GuildMaster guildMaster = GuildMaster.Instance;
        questBoards = guildMaster.QuestBoards;
        employees = guildMaster.Employees;
    }

    /// <summary>
    /// 지원 가기
    /// </summary>
    public void StartApply()
    {
        Debug.Log("지원 시작");
        foreach (QuestBoard questBoard in questBoards)
        {
            if (!questBoard.IsPost()) continue;
            if (questBoard.IsStartAdventure) continue;
            if (questBoard.AfterQuest == null) continue;
            if (questBoard.AfterQuest.IsFullParty()) continue;

            AfterQuest quest = questBoard.AfterQuest;
            float applyRate = questBoard.ApplyRate;

            Debug.Log(string.Format("{0} 퀘스트 지원 시작: 지원률 {1}", quest.Title, applyRate));
            foreach (Adventurer employee in employees)
            {
                // 다른 일 하고 있으면 지원 못함
                if (employee.IsWorking) continue;

                // 퀘스트 지원률에 따라 지원
                float r = Random.Range(0f, 1f);
                if (r <= applyRate)
                {
                    int idx = quest.GetIdxEmptyParty();
                    if (idx != -1)
                    {
                        quest.JoinParty(idx, employee);
                        Debug.Log(string.Format("{0}님이 퀘스트 {1}번:{2} 에 지원하였습니다.",
                            employee.CharName, idx, quest.Title));
                    }
                } 
                else
                {
                    Debug.Log(string.Format("{0}님이 퀘스트를 지원하지 않으셨습니다.", employee.CharName));
                }
            }
        }
        Debug.Log("지원 종료");
    }

    public int ApplyTime { get { return applyTime; } }
}
