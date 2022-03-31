using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 관리 매니저 클래스
/// </summary>
public class GameManager : SingletonBase<GameManager>
{
    private GuildMaster guild;
    
    public void InitGame()
    {
        guild = GuildMaster.Instance;
    }

    public void PauseGame()
    {

    }


    
    public GuildMaster Guild { get { return this.guild; } }
}
