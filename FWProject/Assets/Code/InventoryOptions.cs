using UnityEngine;
public class InventoryOptions : MonoBehaviour
{
    public Inventory inventory;

    public AnimatingInventory animatingInventory;

    public ItemStatistics statistics;

    public int price;

    public void AddItemToInventory()
    {
        int i = Random.Range(0, inventory.UnavailableID);
        if (inventory.items[i].id == 0)
        {
            animatingInventory.creatingItems.AddItem(i, inventory.Data.Item_Data[Random.Range(3, inventory.Data.Item_Data.Count)], 1);
        }
        else
        {
            if (inventory.items.Find(p => p.id == 0) != null)
                AddItemToInventory();
            else
                Debug.Log(message: "Error, Inv is full.");
        }
    }
    public void BuySlot()
    {
        if (int.Parse(inventory.es.currentSelectedGameObject.name) == inventory.UnavailableID)
        {
            animatingInventory.creatingItems.AddItem(inventory.UnavailableID, inventory.Data.Item_Data[0], 1);
            statistics.CountFlorens(price);
            inventory.UnavailableID++;
        }
    }
    public void Shoting()
    {
        int i = Random.Range(0, inventory.UnavailableID);

        if (inventory.items.Find(p => p.id > 7) != null)
        {
            ItemInventory bullet = inventory.items.Find(p => p.id > 7);
            bullet.count--;
        }
        else
        {
            Debug.Log(message: "No ammo in inv.");
        }
    }
    public void SearchForSameItem(Item item, int count) // для стыковки патронов 
    {
        for (int i = 0; i < inventory.maxCount; i++)
        {
            if (inventory.items[i].id == item.id)
            {
                if (inventory.items[0].count < animatingInventory.FullAmmo)
                {
                    inventory.items[i].count += count;
                    if (inventory.items[i].count > animatingInventory.FullAmmo)
                    {
                        count = inventory.items[i].count - animatingInventory.FullAmmo;
                        inventory.items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = inventory.maxCount;
                    }
                }
            }
        }
        if (count > 0)
        {
            for (int i = 0; i < inventory.maxCount; i++)
            {
                if (inventory.items[i].id == 0)
                {
                    animatingInventory.creatingItems.AddItem(i, item, count);
                    i = inventory.maxCount;
                }
            }
        }
    }
}