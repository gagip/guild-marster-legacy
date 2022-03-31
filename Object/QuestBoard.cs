using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour, ITimerEndListener
{
    private const int MAX_LIFE = 3;

    AfterQuest afterQuest;
    [SerializeField] bool isPost;        // 공지 여부
    [SerializeField] bool isStartAdventure;   // 모험 시작
    [SerializeField] [Tooltip("기회")] int life;
    [SerializeField] [Tooltip("지원률 (1=100%를 의미)")] float applyRate;
    [SerializeField] [Tooltip("타이머 시간(단위: 초)")] int time;
    public ResultData resultData;

    [Header("UI 참조변수")]
    [SerializeField] Text titleText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject[] adventurerImgArr;
    [SerializeField] GameObject[] failImgArr;
    [SerializeField] GameObject completeUI;
    [SerializeField] ResultUI resultUI;
    Timer timer;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }

    private void Update()
    {
        int[] hms = timer.getHMS();
        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hms[0], hms[1], hms[2]);
    }

    /// <summary>
    /// 퀘스트 게시판에 공고
    /// </summary>
    public void Post(AfterQuest afterQuest)
    {
        this.afterQuest = afterQuest;
        afterQuest.startQuestEvent += StartAdventure;

        isPost = true;
        gameObject.SetActive(true);
        UpdateUI(afterQuest);
    }

    /// <summary>
    /// 모험 시작
    /// </summary>
    public void StartAdventure()
    {
        
        isStartAdventure = true;
        BattleManager.Instance.questBoard = this;
        if (isStartAdventure)
        {
            timer.startTimer(time, this);
        }
    }

    public bool IsPost()
    {
        return isPost;
    }

    private void UpdateUI(AfterQuest quest)
    {
        titleText.text = quest.Title;
        // 파티원 이미지 출력
        for (int i = 0; i < quest.Party.Length; i++)
        {
            adventurerImgArr[i].SetActive(true);
        }
        for (int i = 0; i < (MAX_LIFE - life); i++)
        {
            failImgArr[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void Click()
    {
        Debug.Log(afterQuest.Title);
    }

    public void OnTimerEnd()
    {
        Debug.Log("타이머 끝");
    }

    public void ChangeComplete(ResultData resultData)
    {
        this.resultData = resultData;
        completeUI.SetActive(true);
    }

    public void ShowResultUI()
    {
        if (resultData != null)
        {
            resultUI.UpdateUI(resultData);
            resultUI.gameObject.SetActive(true);
            
            gameObject.SetActive(false);
            Clear();
        } 
    }

    private void Clear()
    {
        afterQuest = null;
        isPost = false;
        isStartAdventure = false;
        life = MAX_LIFE;
        resultData = null;
        time = 0;
    }

    public bool IsStartAdventure { get { return isStartAdventure; } }
    public float ApplyRate { get { return applyRate; } }
    public AfterQuest AfterQuest { get { return afterQuest; } }
}
