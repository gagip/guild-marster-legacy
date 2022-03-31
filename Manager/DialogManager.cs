using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DialogManager : SingletonBase<DialogManager>
{
    [SerializeField]
    private int branch;
    [SerializeField]
    private DialogDB dialogDB;
    [SerializeField]
    private Speaker[] speakers;
    [SerializeField]
    private DialogData[] dialogs;
    [SerializeField]
    private GameObject dialogUI;
    private bool isAutoStart = true;
    private bool isFirst = true;
    private int currentDialogIdx = -1;
    private int currentSpeakerIdx = 0;

    private float typingSpeed = 0.1f;
    private bool isTypeingEffect = false;

    private void Awake()
    {
        int index = 0;

        foreach (DialogDBEntity dialogDBEntity in dialogDB.tutorial)
        {
            dialogs[index].name = dialogDBEntity.name;
            dialogs[index].dialogue = dialogDBEntity.line;
            dialogs[index].trigger = dialogDBEntity.trigger;
            index++;
        }

        Setup();
    }

    /// <summary>
    /// 대화창 준비
    /// </summary>
    private void Setup()
    {
        dialogUI.SetActive(true);
        foreach (Speaker speaker in speakers)
        {
            SetActiveObjects(speaker, false);
            speaker.spriteRenderer.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 대화 업데이트
    /// </summary>
    /// <returns></returns>
    public bool UpdateDialog()
    {
        if (isFirst == true)
        {
            Setup();

            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTypeingEffect == true)
            {
                isTypeingEffect = false;

                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIdx].textDialogue.text = dialogs[currentDialogIdx].dialogue;

                return false;
            }

            if (dialogs.Length > currentDialogIdx + 1)
            {
                var trigger = dialogs[currentDialogIdx + 1].trigger;
                if (trigger != "")
                {
                    if (trigger == "1")
                    {

                    }
                } 
                
                SetNextDialog();
            }
            else
            {
                CloseDialog();

                return true;
            }
        }
        return false;
    }

    private void SetNextDialog()
    {
        SetActiveObjects(speakers[currentSpeakerIdx], false);

        currentDialogIdx++;
        SetActiveObjects(GetSpeaker(dialogs[currentDialogIdx].name), true);
        speakers[currentSpeakerIdx].textName.text = dialogs[currentDialogIdx].name;
        //speakers[currentSpeakerIdx].textDialogue.text = dialogs[currentDialogIdx].dialogue;
        StartCoroutine("OnTypingText");
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);


        Color color = speaker.spriteRenderer.color;
        color.a = visible == true ? 1 : 0.2f;
        speaker.spriteRenderer.color = color;
    }

    /// <summary>
    /// 대화창 닫기
    /// </summary>
    private void CloseDialog()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
            speakers[i].spriteRenderer.gameObject.SetActive(false);
        }
        dialogUI.SetActive(false);
    }

    /// <summary>
    /// 타자 효과
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypeingEffect = true;
        while (index < dialogs[currentDialogIdx].dialogue.Length)
        {
            speakers[currentSpeakerIdx].textDialogue.text = dialogs[currentDialogIdx].dialogue.Substring(0, index);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTypeingEffect = false;

    }

    /// <summary>
    /// 대화 캐릭터 반환
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private Speaker GetSpeaker(string name)
    {
        foreach (Speaker speaker in speakers)
        {
            if (speaker.name == name)
            {
                return speaker;
            }
        }
        return speakers[0];
    }

    /// <summary>
    /// 해당 오브젝트를 클릭할 때까지 비활성화
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private IEnumerator OnClickObject(GameObject obj)
    {
        yield return null;
    }
}

[System.Serializable]
public struct Speaker
{
    public string name;
    public Image spriteRenderer;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
}

[System.Serializable]
public struct DialogData
{
    public int idx;
    public string name;
    [TextArea(3,5)]
    public string dialogue;
    public string trigger;
}