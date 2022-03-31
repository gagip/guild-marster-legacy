using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeUI : MonoBehaviour
{
    List<Adventurer> employees;

    [SerializeField] CharBox[] charBoxes;

    [SerializeField] Sprite[] images;

    [SerializeField] private Sprite[] boxImgs;

    [SerializeField] private Sprite[] tearMarkImgs;

    void Update()
    {
        Init();
    }

    void Init()
    {
        employees = GuildMaster.Instance.Employees;

        for (int i = 0; i < employees.Count; i++)
        {
            Adventurer employee = employees[i];
            CharBox charBox = charBoxes[i];

            if (employee is Warrior)
            {
                charBox.setImg(images[0]);
            }
            else if (employee is Mage)
            {
                charBox.setImg(images[1]);
            }
            else if (employee is Rogue)
            {
                charBox.setImg(images[2]);
            }
            charBox.setLevelText(employee.Lv);
            charBox.setMark(getTearImg(employee.Tear));
            charBox.setBackground(getBackgroundSprite(employee.Tear));
        }
    }

    private Sprite getBackgroundSprite(TearType tearType)
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

        return boxImgs[imgIdx];
    }
    
    private Sprite getTearImg(TearType tearType)
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

        return tearMarkImgs[imgIdx];
    }
}
