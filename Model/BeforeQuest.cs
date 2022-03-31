using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 의뢰서
/// </summary>
[System.Serializable]
public class BeforeQuest
{
    public string Client { get; set; }          // 의뢰인
    public string Title { get; set; }           // 의뢰 제목
    public string Content { get; set; }         // 의뢰 내용
    public int Retainer { get; set; }           // 의뢰비
    public int Credit { get; set; }             // 신용
    public int Lv { get; set; }                 // 레벨
    public QuestType questType { get; set; }    // 퀘스트 타입

    public BeforeQuest() { }

    public BeforeQuest(int idx)
    {
        SetRequest(idx);
    }

    public void SetRequest(int idx)
    {
        // DB
        DBManager dBManager = DBManager.Instance;
        dBManager.OpenDB("Hunters.db");
        ArrayList data = dBManager.SelectById("Quest", idx, "lv, name, content, gold, type, credit, title");

        // 데이터 가져오기
        Lv = System.Convert.ToInt32(data[0]);
        Client = (string)data[1];
        Content = (string)data[2];
        Retainer = System.Convert.ToInt32(data[3]);
        questType = (QuestType)System.Convert.ToInt32(data[4]);
        Credit = System.Convert.ToInt32(data[5]);
        Title = (string)data[6];
    }
}