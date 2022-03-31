using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] GameObject[] scrollObjs;

    private int[] scrolls = new int[6] { 1, 5, 10, 15, 20, 30 };      // 스크롤 이미지 생기는 기준

    private List<AfterQuest> questList;

    private void Start()
    {
        questList = GuildMaster.Instance.QuestList;
    }

    private void Update()
    {
        UpdateScrollSpite();
    }

    /// <summary>
    /// 퀘스트 개수에 따른 스크롤 이미지 출력
    /// </summary>
    private void UpdateScrollSpite()
    {
        int questCnt = questList.Count;
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrollObjs[i].SetActive(questCnt >= scrolls[i]);
        }
    }

    private void OnMouseDown()
    {
        if (questList.Count > 0)
        {
            QuestUI questUI = UIManager.Instance.QuestUI;
            questUI.Quest = questList[questList.Count - 1];
            questUI.ShowQuestUI(true);
        }
    }
}
