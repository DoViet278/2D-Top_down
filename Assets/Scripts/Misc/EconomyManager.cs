using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;    

    const string GOLD_TEXT_PATH = "GoldAmountText";
    public void UpdateCurrentGold()
    {
        currentGold += 1;

        if(goldText == null)
        {
            goldText = GameObject.Find(GOLD_TEXT_PATH).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}
