using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMen : MonoBehaviour
{
    [SerializeField] GameObject[] clients;

    private List<Adventurer> adventurers;
    private int[] adventurerThresholdArr;

    private void Start()
    {
        adventurers = GuildMaster.Instance.Adventurers;
        adventurerThresholdArr = new int[] { 1, 5, 10, 15, 20 };
    }

    /// <summary>
    /// 모험가들 줄서기
    /// </summary>
    public void StackRequest()
    {
        for (int i = 0; i < adventurerThresholdArr.Length; i++)
        {
            int adventurerCnt = adventurers.Count;
            bool isFlag = adventurerCnt >= adventurerThresholdArr[i];
            clients[i].SetActive(isFlag);
        }
    }

    public void ShowAdventurerUI()
    {
        if (adventurers.Count < 1) return;

        AdventurerUI adventurerUI = UIManager.Instance.AdventurerUI;
        Adventurer adventurer = adventurers[0];
        adventurerUI.Adventurer = adventurer;
        adventurerUI.ShowUI(true);
    }
}
