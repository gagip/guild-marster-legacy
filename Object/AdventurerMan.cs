using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMan : MonoBehaviour
{
    private AdventurerMen adventureMen;

    // Start is called before the first frame update
    void Start()
    {
        adventureMen = GetComponentInParent<AdventurerMen>();
    }

    private void OnMouseDown()
    {
        adventureMen.ShowAdventurerUI();
    }
}
