using UnityEngine;
using UnityEngine.UI;
using TMPro;
public partial class Inventory : MonoBehaviour
{
    public void AddItem(int id, Item item, int count) //считывание параметров предмета для добавления обьекта в инвентарь 
    {
        items[id].id = item.id; // ID предмета в инвентаре
        items[id].count = count; // число предмета в инвентаре
        items[id].itemobj.GetComponent<Image>().sprite = item.img; // спрайт предмета

        if (count > 1 && item.id != 0)
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = count.ToString();
        }
        else
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = " ";
        }
    }
    public void RemoveItem()
    {
        int i = Random.Range(0, UnavailableID);
        if (items[i].id > 2)
            AddItem(i, Data.Item_Data[0], 1);
        else
        {
            if (items.Find(p => p.id > 2) != null)
                RemoveItem();
            else
                Debug.Log(message: "Error, Inv is empty.");
        }
    }
    public void AddBullet()
    {
        int i = Random.Range(0, UnavailableID);
        if (items[i].id == 0)
            AddItem(i, Data.Item_Data[Random.Range(9, Data.Item_Data.Count)], Random.Range(0, 99));
        else
        {
            if (items.Find(p => p.id == 0) != null)
                AddBullet();
            else
                Debug.Log(message: "Error, Inv is full.");
        }
    }

}