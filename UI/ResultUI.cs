using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI monsterText;
    public Image[] itemImages;
    public Image stamp;
    public Sprite[] stampImg;

    public void UpdateUI(ResultData resultData)
    {
        UpdateMonsterText(resultData.monstarText);
        UpdateItemImage(resultData.items);
        UpdateStamp(resultData.isSuccess);
    }

    private void UpdateMonsterText(string monsterText)
    {
        this.monsterText.text = monsterText;
    }

    private void UpdateItemImage(List<Item> items)
    {
        foreach (var item in items)
        {
            Image itemImage = itemImages.ToList().Find(x => x.gameObject.activeSelf == false);

            if (itemImage != null)
            {
                itemImage.sprite = item.sprite;
                itemImage.gameObject.SetActive(true);
            }
        }

    }

    private void UpdateStamp(bool isSuccess)
    {
        stamp.sprite = isSuccess ? stampImg[0] : stampImg[1];
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }
}
