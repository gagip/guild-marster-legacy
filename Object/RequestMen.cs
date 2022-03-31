using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMen : MonoBehaviour
{
    [SerializeField] GameObject[] clients;

    private List<BeforeQuest> requestList;
    private int[] requestThresholdArr;

    private void Start()
    {
        requestList = GuildMaster.Instance.RequestList;
        requestThresholdArr = new int[] { 1, 5, 10, 15, 20 };
    }

    /// <summary>
    /// 의뢰하는 사람들 줄서기
    /// </summary>
    public void StackRequest()
    {
        for (int i = 0; i < requestThresholdArr.Length; i++)
        {
            int requestCnt = requestList.Count;
            bool isFlag = requestCnt >= requestThresholdArr[i];
            clients[i].SetActive(isFlag);
        }
    }

    public void ShowRequestUI()
    {
        if (requestList.Count < 1) return;

        RequestUI requestUI = UIManager.Instance.RequestUI;
        requestUI.ShowRequestUI(true);
    }
}
