using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public List<Item> Item_Data = new List<Item>();
}

[System.Serializable]

public class Item
{
    public string name;
    public int id;
    public Sprite img;
    public string dmg, weight, resist;

}