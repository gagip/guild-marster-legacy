using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RequestUI : MonoBehaviour
{

    [SerializeField] GameObject pnl;
    [SerializeField] Text requestCntTxt;
    [SerializeField] TextMeshProUGUI requestNameTxt;
    [SerializeField] TextMeshProUGUI requestContentTxt;
    [SerializeField] TextMeshProUGUI requestRetainerTxt;
    [SerializeField] TextMeshProUGUI requestSignTxt;

    private int requestIdx;
    private List<BeforeQuest> requestList;

    void Start()
    {
        requestList = GuildMaster.Instance.RequestList;
    }

    private void Update()
    {
        requestCntTxt.text = string.Format("{0}/{1}", requestIdx + 1, requestList.Count);
    }

    private void UpdateRequestUI(int requestIdx)
    {
        BeforeQuest request = requestList[requestIdx];
        requestNameTxt.text = request.Client;
        requestContentTxt.text = request.Content;
        requestRetainerTxt.text = request.Retainer.ToString();
        requestSignTxt.text = request.Client;
    }

    /// <summary>
    /// 이전 의뢰서로 이동
    /// </summary>
    public void BackRequest()
    {
        RequestIdx--;
        UpdateRequestUI(RequestIdx);
    }

    /// <summary>
    /// 다음 의뢰서로 이동
    /// </summary>
    public void NextRequest()
    {
        RequestIdx++;
        UpdateRequestUI(RequestIdx);
    }

    /// <summary>
    /// 의뢰서 승낙
    /// </summary>
    public void AcceptRequest()
    {
        // 퀘스트 UI로 전환
        GuildMaster.Instance.ReceiveQuest(requestList[requestIdx]);
        ShowRequestUI(false);
    }

    /// <summary>
    /// 의뢰서 거부
    /// </summary>
    public void RejectRequest()
    {
        requestList.RemoveAt(RequestIdx);
        ShowRequestUI(false);
    }

    public void ShowRequestUI(bool value)
    {
        pnl.SetActive(value);
    }

    #region property
    private int RequestIdx
    {
        get
        { return requestIdx; }
        set
        {
            if (value < 0) requestIdx = 0;
            else if (value >= requestList.Count) requestIdx = requestList.Count - 1;
            else requestIdx = value;
        }
    }
    #endregion
}
