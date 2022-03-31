using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;
using UnityEngine.UI;

public class BattleManager : SingletonBase<BattleManager>
{
    private List<Adventurer> adventurers;       // 모험가
    private List<Monster> monsters;             // 몬스터
    public QuestBoard questBoard;               // 퀘스트 보드


    [SerializeField] LogUI logUI;               // 로그 UI
    private string logContent;
    [SerializeField] ResultUI resultUI;
    [SerializeField] GameObject panel;
    [SerializeField] Sprite itemSprite;
    [SerializeField] Image itemImage;
    public bool isSuccess;

    public List<Item> items = new List<Item>();

    public void Init()
    {
        //monsters.Add(new Slime());
        //monsters.Add(new Slime());
        //monsters.Add(new Slime());
        Debug.Log(adventurers.ToString());
    }

    public void StartBattle()
    { 
    }

    public void StartBattle(List<Adventurer> adventurers)
    {
        this.adventurers = adventurers;
        this.monsters = new List<Monster>();
        Init();

        Round round = new Round(this.adventurers, this.monsters);
        round.RoundEndEvent += RoundEnd;
        round.StartRound();
    }
    
    public void AddLog(string log)
    {
        this.logContent += log + "\n";
    }

    public void RoundEnd()
    {
        logUI.SetText(logContent);
        string monsterTxt = "";
        if (monsters != null)
        {
            monsters.ForEach(x => {
                monsterTxt += x.CharName + "   ";
            });
        }
        ResultData resultData = new ResultData();
        resultData.monstarText = monsterTxt;
        resultData.items = items;
        resultData.isSuccess = isSuccess;
        questBoard.ChangeComplete(resultData);
    }
}
