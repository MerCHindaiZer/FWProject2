using UnityEngine;
using UnityEngine.UI;
using TMPro;
public partial class Inventory : MonoBehaviour
{
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObj.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }
    public void SetStatistics(int id)
    {
        Name.text = "Name: " + Data.Item_Data[id].name;
        Weight.text = "Weight: " + Data.Item_Data[id].weight;
        DMG.text = "DMG: " + Data.Item_Data[id].dmg;
        Resist.text = "Resist: " + Data.Item_Data[id].resist;
    }
    public void SelectObject()
    {
        if (currentID == -1)
        {
            if (int.Parse(es.currentSelectedGameObject.name) < UnavailableID)
            {
                currentID = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentID]);
                movingObject.GetComponent<Image>().sprite = Data.Item_Data[currentItem.id].img;
                movingObject.gameObject.SetActive(true);
                AddItem(currentID, Data.Item_Data[0], 0);

                SetStatistics(currentItem.id);

            }
            else if (int.Parse(es.currentSelectedGameObject.name) == UnavailableID)
                BuySlot();
        }
        else
        {
            if (int.Parse(es.currentSelectedGameObject.name) < UnavailableID)
            {
                ItemInventory ItemInv = items[int.Parse(es.currentSelectedGameObject.name)];
                if (currentItem.id != ItemInv.id)
                {
                    AddInventoryItem(currentID, ItemInv);
                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (ItemInv.count + currentItem.count <= FullAmmo)
                    {
                        ItemInv.count += currentItem.count;
                    }
                    else
                    {
                        AddItem(currentID, Data.Item_Data[ItemInv.id], ItemInv.count + currentItem.count - FullAmmo);

                        ItemInv.count = FullAmmo;
                    }
                }
                SetStatistics(0);
                currentID = -1;
                movingObject.gameObject.SetActive(false);
            }
        }
    }
    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObj.transform) as GameObject;
            newItem.name = i.ToString();
            ItemInventory ItemInv = new ItemInventory();
            ItemInv.itemobj = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate{ SelectObject();});

            items.Add(ItemInv); //добавление предмета в инвентарь
        }
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].count != 0 && items[i].count > 1)
            {
                items[i].itemobj.GetComponentInChildren<TMP_Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemobj.GetComponentInChildren<TMP_Text>().text = "";
            }
            items[i].itemobj.GetComponent<Image>().sprite = Data.Item_Data[items[i].id].img;

            money.text = "florens: " + florens.ToString();
        }
        if (UnavailableID != 32)
            AddItem(UnavailableID, Data.Item_Data[2], 1);
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
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemobj.GetComponent<Image>().sprite = Data.Item_Data[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = "";
        }
    }
}