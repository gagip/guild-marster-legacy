using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogUI : MonoBehaviour
{

    public TextMeshProUGUI logText;
    public int page;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetText(string text)
    {
        logText.text = text;
        page++;
    }

    public void UpdateUI()
    {
        
    }

    public void MovePreviousPage()
    {
        Debug.Log("���� �������� �̵�");
    }

    public void MoveNextPage()
    {
        Debug.Log("���� �������� �̵�");        
    }
}
