using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AnimatingInventory : MonoBehaviour
{
    public Inventory inventory;

    public CreatingItems creatingItems;

    public InventoryOptions options;

    public ItemStatistics statistics;

    public Vector3 offset;

    public GameObject gameObjShow, InventoryMainObj;

    public Camera cam;

    public RectTransform movingObject;

    public ItemInventory currentItem;

    public int FullAmmo;

    public void Start()
    {
        cam = Camera.FindFirstObjectByType<Camera>();
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;

        pos.z = InventoryMainObj.GetComponent<RectTransform>().position.z;

        movingObject.position = cam.ScreenToWorldPoint(pos);
    }
    public void SelectObject()
    {
        if (inventory.currentID == -1)
        {
            if (int.Parse(inventory.es.currentSelectedGameObject.name) < inventory.UnavailableID)
            {
                inventory.currentID = int.Parse(inventory.es.currentSelectedGameObject.name);

                currentItem = CopyInventoryItem(inventory.items[inventory.currentID]);

                movingObject.GetComponent<Image>().sprite = inventory.Data.Item_Data[currentItem.id].img;

                movingObject.gameObject.SetActive(true);

                creatingItems.AddItem(inventory.currentID, inventory.Data.Item_Data[0], 0);

                statistics.SetStatistics(currentItem.id);
            }
            else if (int.Parse(inventory.es.currentSelectedGameObject.name) == inventory.UnavailableID)
            {
                options.BuySlot();
            }
        }
        else
        {
            if (int.Parse(inventory.es.currentSelectedGameObject.name) < inventory.UnavailableID)
            {
                ItemInventory ItemInv = inventory.items[int.Parse(inventory.es.currentSelectedGameObject.name)];

                if (currentItem.id != ItemInv.id)
                {
                    AddInventoryItem(inventory.currentID, ItemInv);

                    AddInventoryItem(int.Parse(inventory.es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (ItemInv.count + currentItem.count <= FullAmmo)
                    {
                        ItemInv.count += currentItem.count;
                    }
                    else
                    {
                        creatingItems.AddItem(inventory.currentID, inventory.Data.Item_Data[ItemInv.id], ItemInv.count + currentItem.count - FullAmmo);

                        ItemInv.count = FullAmmo;
                    }
                }
               statistics.SetStatistics(0);

                inventory.currentID = -1;

                movingObject.gameObject.SetActive(false);
            }
        }
    }
    public void AddGraphics() // создания слотов в инвентаре
    {
        for (int i = 0; i < inventory.maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObj.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ItemInv = new ItemInventory();

            ItemInv.itemobj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate{ SelectObject();});

            inventory.items.Add(ItemInv);
        }
    }
    public void UpdateInventory() // для визуализации предмета 
    {
        for (int i = 0; i < inventory.maxCount; i++)
        {
            VisualizateItemCount(i);

            inventory.items[i].itemobj.GetComponent<Image>().sprite = inventory.Data.Item_Data[inventory.items[i].id].img;

            if (inventory.UnavailableID != 32)
                creatingItems.AddItem(inventory.UnavailableID, inventory.Data.Item_Data[2], 1);
        }
    }
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;

        New.itemobj = old.itemobj;

        New.count = old.count;

        return New;
    }
    public void AddInventoryItem(int id, ItemInventory invItem) //считывание параметров предмета для переноса его по инвентарю
    {
        inventory.items[id].id = invItem.id;

        inventory.items[id].count = invItem.count;

        inventory.items[id].itemobj.GetComponent<Image>().sprite = inventory.Data.Item_Data[invItem.id].img;

        VisualizateItemCount(id);
    }
    public void VisualizateItemCount(int id)
    {
        if (inventory.items[id].count > 1 && inventory.items[id].id != 0)
        {
            inventory.items[id].itemobj.GetComponentInChildren<TMP_Text>().text = inventory.items[id].count.ToString();
        }
        else
        {
            inventory.items[id].itemobj.GetComponentInChildren<TMP_Text>().text = "";
        }
    }
}