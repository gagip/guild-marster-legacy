using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharBox : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] private Image mark;

    public void setImg(Sprite sprite)
    {
        image.sprite = sprite;
        image.color = Color.white;
    }

    public void setLevelText(int level)
    {
        levelText.text = level + "";
    }

    public void setBackground(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void setMark(Sprite sprite)
    {
        mark.sprite = sprite;
        image.color = Color.white;
    }

    public void clear()
    {
        image.sprite = null;
        image.color = Color.clear;
        levelText.text = "";
    }
}
