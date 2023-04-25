using UnityEngine;
public class RenderingInventory : MonoBehaviour
{
    public Inventory inventory;

    public AnimatingInventory animatingInventory;

    public ItemStatistics statistics;

    public CreatingItems creatingItems;

    public void Start()
    {
        if (inventory.items.Count == 0)
        {
            animatingInventory.AddGraphics();
        }
        for (int i = inventory.UnavailableID; i < inventory.maxCount; i++)
        {
            creatingItems.AddItem(i, inventory.Data.Item_Data[1], 1);
        }
    }
    public void Update()
    {
        if (inventory.currentID != -1)
        {
            animatingInventory.MoveObject();
        }
        animatingInventory.UpdateInventory();
        statistics.UpdateFlorens();
    }

}
