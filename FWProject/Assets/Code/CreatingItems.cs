using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CreatingItems : MonoBehaviour
{
    public Inventory inventory;
    public AnimatingInventory animatingInventory;
    public DataBase Data;

    public void AddItem(int id, Item item, int count) //считывание параметров предмета для добавления обьекта в инвентарь 
    {
        inventory.items[id].id = item.id; // ID предмета в инвентаре
        inventory.items[id].count = count; // число предмета в инвентаре
        inventory.items[id].itemobj.GetComponent<Image>().sprite = item.img; // спрайт предмета

        if (count > 1 && item.id != 0)
        {
            inventory.items[id].itemobj.GetComponentInChildren<TMP_Text>().text = count.ToString();
        }
        else
        {
            inventory.items[id].itemobj.GetComponentInChildren<TMP_Text>().text = " ";
        }
    }
    public void RemoveItem()
    {
        int i = Random.Range(0, inventory.UnavailableID);
        if (inventory.items[i].id > 2)
            AddItem(i, Data.Item_Data[0], 1);
        else
        {
            if (inventory.items.Find(p => p.id > 2) != null)
                RemoveItem();
            else
                Debug.Log(message: "Error, Inv is empty.");
        }
    }
    public void AddBullet()
    {
        int i = Random.Range(0, inventory.UnavailableID);
        if (inventory.items[i].id == 0)
            AddItem(i, Data.Item_Data[Random.Range(9, Data.Item_Data.Count)], Random.Range(0, 99));
        else
        {
            if (inventory.items.Find(p => p.id == 0) != null)
                AddBullet();
            else
                Debug.Log(message: "Error, Inv is full.");
        }
    }

}