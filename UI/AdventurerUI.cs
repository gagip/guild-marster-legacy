using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventurerUI : MonoBehaviour
{
    [SerializeField] Adventurer adventurer;
    [SerializeField] Sprite[] adventurerImg;
    [SerializeField] Sprite[] backgroundImg;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI strText;
    [SerializeField] TextMeshProUGUI conText;
    [SerializeField] TextMeshProUGUI intelText;
    [SerializeField] TextMeshProUGUI spiText;
    [SerializeField] TextMeshProUGUI agiText;
    [SerializeField] Image profilePictureImg;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    
    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        GetComponent<Image>().sprite = backgroundImg[0];
        
        nameText.text = "";
        levelText.text = "";
        strText.text = "";
        conText.text = "";
        intelText.text = "";
        spiText.text = "";
        agiText.text = "";
        profilePictureImg.sprite = null;

        if (adventurer != null)
        {
            UpdateUI(adventurer);
        }
    }

    public void UpdateUI(Adventurer adventurer)
    {
        this.adventurer = adventurer;
        Debug.Log(adventurer);

        PrintAdventurerInfo(adventurer);
        SetAventurerPicture(adventurer);
        SetBackground(adventurer.Tear);
    }

    public void ShowUI(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnPressPositiveButton()
    {
        GuildMaster.Instance.Adventurers.Remove(adventurer);
        GuildMaster.Instance.Employees.Add(adventurer);
        adventurer = null;
        ShowUI(false);
    }

    public void OnPressNegativeButton()
    {
        GuildMaster.Instance.Adventurers.Remove(adventurer);
        ShowUI(false);
    }

    private void PrintAdventurerInfo(Adventurer adventurer)
    {
        nameText.text = adventurer.CharName;
        levelText.text = adventurer.Lv + "";
        strText.text = adventurer.Str + "";
        conText.text = adventurer.Con + "";
        intelText.text = adventurer.Intel + "";
        spiText.text = adventurer.Spi + "";
        agiText.text = adventurer.Agi + "";
    }

    private void SetAventurerPicture(Adventurer adventurer)
    {
        if (adventurer is Rogue)
        {
            profilePictureImg.sprite = adventurerImg[0];
        } 
        else if (adventurer is Warrior)
        {
            profilePictureImg.sprite = adventurerImg[1];
        }
        else if (adventurer is Mage)
        {
            profilePictureImg.sprite = adventurerImg[2];
        }
    }

    private void SetBackground(TearType tearType)
    {
        int imgIdx;
        
        switch (tearType)
        {
            case TearType.IRON:
                imgIdx = 0;
                break;
            case TearType.BRONZE:
                imgIdx = 1;
                break;
            case TearType.SILVER:
                imgIdx = 2;
                break;
            case TearType.GOLD:
                imgIdx = 3;
                break;
            case TearType.PLATINUM:
                imgIdx = 4;
                break;
            default:
                imgIdx = 0;
                break;
        }
        
        GetComponent<Image>().sprite = backgroundImg[imgIdx];
    }

    public Adventurer Adventurer { set { adventurer = value; } }
}
