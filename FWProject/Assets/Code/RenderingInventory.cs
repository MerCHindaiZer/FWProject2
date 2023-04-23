using UnityEngine;
public partial class Inventory : MonoBehaviour
{
    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }
        for (int i = UnavailableID; i < maxCount; i++)
        {
            AddItem(i, Data.Item_Data[1], 1);
        }
    }
    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }
        UpdateInventory();
    }

}
