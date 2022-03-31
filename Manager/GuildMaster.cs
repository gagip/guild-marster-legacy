using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildMaster : SingletonBase<GuildMaster>
{
    [SerializeField]
    [Tooltip("퀘스트 한 개 받는 시간")]
    const float RECIVE_QUEST_TIME = 10f;
    [SerializeField]
    [Tooltip("모험가 한 명 모집 시간")]
    const float RECIVE_ADVENTURE_TIME = 10f;

    [Header("자원")]
    [SerializeField] int gold = 10000;
    [SerializeField] int credit = 100;
    [SerializeField] int day = 1;

    private readonly List<BeforeQuest> requestList = new List<BeforeQuest>();
    private readonly List<AfterQuest> questList = new List<AfterQuest>();
    private readonly List<Adventurer> adventurers = new List<Adventurer>();
    private readonly List<Adventurer> employees = new List<Adventurer>();
    [SerializeField] RequestMen requestClients;
    [SerializeField] AdventurerMen adventurerClients;
    [SerializeField] List<QuestBoard> questBoardList;

    
    private ApplyManager applyManager;

    void Start()
    {
        applyManager = ApplyManager.Instance;

        StartCoroutine(ReceiveBeforeQuest());
        StartCoroutine(ReceiveAdventurer());
        StartCoroutine(ApplyQuest());
    }

    void Update()
    {
        requestClients.StackRequest();
        adventurerClients.StackRequest();
    }

    /// <summary>
    /// 주기적으로 의뢰 받기
    /// </summary>
    IEnumerator ReceiveBeforeQuest()
    {
        int maxBeforeQuestCnt = 14;
        do
        {
            if (requestList.Count < maxBeforeQuestCnt)
            {
                int randNum = Random.Range(1, maxBeforeQuestCnt);
                BeforeQuest quest = new BeforeQuest(randNum);
                requestList.Add(quest);
            }

            yield return new WaitForSeconds(RECIVE_QUEST_TIME);
        } while (true);
    }

    /// <summary>
    /// 모험가 구하기
    /// </summary>
    /// <returns></returns>
    IEnumerator ReceiveAdventurer()
    {
        int maxAdventurerCnt = 5;
        do
        {
            if (employees.Count < maxAdventurerCnt)
            {
                var adventurer = AdventurerFactory.GetAdventurer();
                adventurers.Add(adventurer);
            }

            yield return new WaitForSeconds(RECIVE_ADVENTURE_TIME);
        } while (true);
    }

    /// <summary>
    /// 고용자들이 퀘스트 지원 시작 
    /// </summary>
    IEnumerator ApplyQuest()
    {
        do
        {
            int applyTime = applyManager.ApplyTime;
            applyManager.StartApply();
            yield return new WaitForSeconds(applyTime);
        } while (true);
    }

    /// <summary>
    /// 퀘스트 받기
    /// /// </summary>
    public void ReceiveQuest(BeforeQuest request)
    {
        requestList.Remove(request);
        AfterQuest quest = new AfterQuest(request);
        questList.Add(quest);
    }

    /// <summary>
    /// 퀘스트를 길드원에게 공유
    /// </summary>
    public void Notice(AfterQuest quest)
    {
        int idx = GetEmptyQuestBoard();

        if (idx != -1)
        {
            QuestBoard questBoard = questBoardList[idx];
            questBoard.Post(quest);
            questList.Remove(quest);
        }
        else
        {
            Debug.Log("퀘스트 게시판이 가득 찼습니다");
        }
    }

    /// <summary>
    /// 비어있는 퀘스트 게시글을 찾기
    /// </summary>
    /// <returns></returns>
    int GetEmptyQuestBoard()
    {
        for (int i = 0; i < questBoardList.Count; i++)
        {
            bool isActive = questBoardList[i].IsPost();
            if (!isActive) return i;
        }
        return -1;
    }

    #region property
    public int Gold
    {
        get { return gold; }
        set { gold = value > 0 ? value : 0; }
    }
    public int Credit
    {
        get { return credit; }
        set { credit = value > 0 ? value : 0; }
    }
    public int Day { get { return day; } }
    public List<BeforeQuest> RequestList { get { return requestList; } }
    public List<AfterQuest> QuestList { get { return questList; } }
    public List<QuestBoard> QuestBoards { get { return questBoardList; } }
    public List<Adventurer> Adventurers { get { return adventurers; } }
    public List<Adventurer> Employees { get { return employees; } }
    #endregion
}
