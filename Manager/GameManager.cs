using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �Ŵ��� Ŭ����
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
