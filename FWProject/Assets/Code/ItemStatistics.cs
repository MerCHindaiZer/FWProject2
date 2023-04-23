using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemStatistics : MonoBehaviour
{
    public DataBase Data;
    public TMP_Text Name, Weight, DMG, Resist;
    public TMP_Text money;
    public int florens;
    public void SetStatistics(int id)
    {
        Name.text = "Name: " + Data.Item_Data[id].name;
        Weight.text = "Weight: " + Data.Item_Data[id].weight;
        DMG.text = "DMG: " + Data.Item_Data[id].dmg;
        Resist.text = "Resist: " + Data.Item_Data[id].resist;

    }
    public void UpdateFlorens()
    {
        money.text = "florens: " + florens.ToString();
    }
    public void CountFlorens(int price)
    {
        florens -= price;
    }
}
