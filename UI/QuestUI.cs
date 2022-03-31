using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public static QuestUI instance;
    [SerializeField] private const int GOLD_UNIT = 100;
    private const string GOLD = "Gold";

    private AfterQuest quest;
    private bool isRecon;
    private int pay;
    private int partyMember;
    private bool[] recommend;

    [SerializeField] TextMeshProUGUI questContentTxt;
    [SerializeField] TextMeshProUGUI questTypeTxt;
    [SerializeField] TextMeshProUGUI questPayTxt;
    [SerializeField] Image[] partyMemberImg;
    [SerializeField] Image checkBoxImg;
    [SerializeField] Sprite[] checkBoxImgs;     // 0: 비활성, 1: 활성

    void Start()
    {
        init();
    }

    private void OnEnable()
    {
        init();
    }

    public void ShowQuestUI(bool value)
    {
        gameObject.SetActive(value);
    }

    private void init()
    {
        // 입력값 초기화
        isRecon = false;
        pay = 0;
        partyMember = 0;
        recommend = new bool[5];

        UpdateQuestUI();
    }

    /// <summary>
    /// 보수 증가
    /// </summary>
    public void IncreasePay()
    {
        Pay += GOLD_UNIT;
        UpdatePayText(Pay);
    }

    /// <summary>
    /// 보수 감소
    /// </summary>
    public void DecreasePay()
    {
        Pay -= GOLD_UNIT;
        UpdatePayText(Pay);
    }

    /// <summary>
    /// 인원수 증가
    /// </summary>
    public void AddPartyCapacity()
    {
        PartyMember += 1;
        UpdatePartyMemberSprite();
    }

    /// <summary>
    /// 인원수 감소
    /// </summary>
    public void RemovePartyCapacity()
    {
        PartyMember -= 1;
        UpdatePartyMemberSprite();
    }

    /// <summary>
    /// 퀘스트 확인
    /// </summary>
    public void AcceptQuest()
    {
        // 제약조건
        if (partyMember <= 0)
        {
            Debug.Log("모집인원은 1명 이상입니다.");
            return;
        }
        // 입력값 퀘스트 객체에 전달
        quest.Pay = pay;
        quest.IsRecon = isRecon;
        quest.Party = new Adventurer[partyMember];
        // UI 창
        GuildMaster.Instance.Notice(quest);
        ShowQuestUI(false);
    }

    /// <summary>
    /// 퀘스트 취소
    /// </summary>
    public void CancelQuest()
    {
        // 퀘스트 제거
        if (quest != null)
        {
            GuildMaster.Instance.QuestList.Remove(quest);
        }

        // UI 창 닫기
        ShowQuestUI(false);
    }

    public void SetRecon()
    {
        quest.IsRecon = !quest.IsRecon;
        UpdateReconUI();
    }

    private void UpdateQuestUI()
    {
        UpdateContentUI();
        UpdateQuestType();
        UpdatePayText(pay);
        UpdatePartyMemberSprite();
        UpdateReconUI();
    }

    private void UpdateQuestType()
    {
        string str = "";

        switch (quest.questType)
        {
            case QuestType.ESCORT:
                str = "호위";
                break;
            case QuestType.EXPEDITION:
                str = "원정";
                break;
            case QuestType.COLLECTION:
                str = "수집";
                break;
        }

        questTypeTxt.text = str;
    }

    private void UpdateContentUI()
    {
        questContentTxt.text = quest.Content;
    }

    private void UpdatePartyMemberSprite()
    {
        for (int i = 0; i < partyMemberImg.Length; i++)
        {
            Image img = partyMemberImg[i];
            if (i < partyMember)
            {
                img.color = Color.black;
            }
            else
            {
                img.color = Color.white;
            }
        }
    }

    private void UpdateReconUI()
    {
        checkBoxImg.sprite = quest.IsRecon ? checkBoxImgs[1] : checkBoxImgs[0];
    }

    private void UpdatePayText(int pay)
    {
        questPayTxt.text = pay.ToString() + GOLD;
    }

    #region property
    private int Pay
    {
        get { return pay; }
        set
        {
            if (value <= 0) pay = 0;
            else pay = value;
        }
    }

    private int PartyMember
    {
        get { return partyMember; }
        set
        {
            if (value <= 0)
                partyMember = 0;
            else if (value > 5)
                partyMember = 5;
            else
                partyMember = value;
        }
    }

    public AfterQuest Quest
    {
        get { return quest; }
        set { quest = value; }
    }
    #endregion
}
