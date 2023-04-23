using UnityEngine;
public partial class Inventory : MonoBehaviour
{
    public void AddItemToInventory()
    {
        int i = Random.Range(0, UnavailableID);
        if (items[i].id == 0)
            AddItem(i, Data.Item_Data[Random.Range(3, Data.Item_Data.Count)], 1);
        else
        {
            if (items.Find(p => p.id == 0) != null)
                AddItemToInventory();
            else
                Debug.Log(message: "Error, Inv is full.");
        }
    }
    public void BuySlot()
    {
        if (int.Parse(es.currentSelectedGameObject.name) == UnavailableID)
        {
            AddItem(UnavailableID, Data.Item_Data[0], 1);
            florens -= price;
            UnavailableID++;
        }
    }
    public void Shoting()
    {
        int i = Random.Range(0, UnavailableID);

        if (items.Find(p => p.id > 7) != null)
        {
            ItemInventory bullet = items.Find(p => p.id > 7);
            bullet.count--;
        }
        else
            Debug.Log(message: "No ammo in inv.");
    }
    public void SearchForSameItem(Item item, int count) // для стыковки патронов 
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < FullAmmo)
                {
                    items[i].count += count;
                    if (items[i].count > FullAmmo)
                    {
                        count = items[i].count - FullAmmo;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }
}