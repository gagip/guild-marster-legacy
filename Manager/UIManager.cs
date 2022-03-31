using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonBase<UIManager>
{
    // 텍스트 변수
    [SerializeField] TextMeshProUGUI goldTxt;
    [SerializeField] TextMeshProUGUI creditTxt;

    [SerializeField] RequestUI requestUI;
    [SerializeField] QuestUI questUI;
    [SerializeField] AdventurerUI adventurerUI;

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        goldTxt.text = GuildMaster.Instance.Gold.ToString();
        creditTxt.text = GuildMaster.Instance.Credit.ToString();
    }

    #region property
    public RequestUI RequestUI { get { return requestUI; } }

    public QuestUI QuestUI { get { return questUI; } }

    public AdventurerUI AdventurerUI { get { return adventurerUI; } }
    #endregion
}
