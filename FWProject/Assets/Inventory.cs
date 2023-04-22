using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public DataBase Data; // хранилище данных о вещах, ID, спрайты и наименовани€
    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjShow, InventoryMainObj; // дл€ настройки инвентар€

    public int maxCount; // число слотов

    public int currentID; // ID €чейки

    public int UnavailableID; // ID недоступных €чеек

    public ItemInventory currentItem;

    public EventSystem es;

    public RectTransform movingObject;
    public Vector3 offset;
    public Camera cam; // дл€ следовани€ предмета за курсором

    public int florens; //монеты
    public TMP_Text money,Name,Weight,DMG,Resist;
    public int price;
    public int FullAmmo;

    public void Start()
    {
        
        if(items.Count == 0)
        {
            AddGraphics();

        }

        for(int i = UnavailableID; i < maxCount; i++)
        {
            AddItem(i,Data.Item_Data[1],1);
            
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

    public void AddItem(int id, Item item, int count) //считывание параметров предмета дл€ добавлени€ обьекта в инвентарь 
    {
        items[id].id = item.id; // ID предмета в инвентаре
        items[id].count = count; // число предмета в инвентаре
        items[id].itemobj.GetComponent<Image>().sprite = item.img; // спрайт предмета

        if(count > 1 && item.id != 0)
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = count.ToString();
        }
        else
        {
            items[id].itemobj.GetComponentInChildren<TMP_Text>().text = " ";

        }

    }

    public void AddInventoryItem(int id, ItemInventory invItem) //считывание параметров предмета дл€ переноса его по инвентарю
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

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObj.transform) as GameObject;
            newItem.name = i.ToString();
            ItemInventory ii = new ItemInventory();
            ii.itemobj = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii); //добавление предмета в инвентарь


        }
    }

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
        if(currentID == -1 )
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
                ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
                if(currentItem.id != II.id)
                {
                    AddInventoryItem(currentID,II);
                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if(II.count + currentItem.count <= FullAmmo)
                    {
                        II.count += currentItem.count;
                    }
                    else
                    {
                        AddItem(currentID,Data.Item_Data[II.id], II.count + currentItem.count - FullAmmo);

                        II.count = FullAmmo;
                    }
                }

                SetStatistics(0);
                currentID = -1;
                movingObject.gameObject.SetActive(false);
            }
        }
        
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].count != 0 && items[i].count > 1)
            {
                items[i].itemobj.GetComponentInChildren<TMP_Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemobj.GetComponentInChildren<TMP_Text>().text = "";
            }
            items[i].itemobj.GetComponent<Image>().sprite= Data.Item_Data[items[i].id].img;

            money.text = "florens: " + florens.ToString();
            
        }
        if(UnavailableID != 32)
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

    public void Add_Item()
    {
        
        int i = Random.Range(0,UnavailableID);
        if (items[i].id == 0)
            AddItem(i, Data.Item_Data[Random.Range(3, Data.Item_Data.Count)], 1);
        else
        {
            if(items.Find(p => p.id == 0) != null)  
                Add_Item();
            else
                Debug.Log(message: "Error, Inv is full.");                
        }
    }

    public void DeleteItem()
    {
        int i = Random.Range(0, UnavailableID);
        if (items[i].id > 2)
            AddItem(i, Data.Item_Data[0], 1);
        else
        {
            if (items.Find(p => p.id > 2) != null)
                DeleteItem();
            else
                Debug.Log(message: "Error, Inv is empty."); 
        }
    }

    public void AddBullet()
    {
        int i = Random.Range(0, UnavailableID);
        if (items[i].id == 0)
            AddItem(i, Data.Item_Data[Random.Range(9, Data.Item_Data.Count)], Random.Range(0,99));
        else
        {
            if (items.Find(p => p.id == 0) != null)
                AddBullet();
            else
                Debug.Log(message: "Error, Inv is full.");
        }
    }
    public void BuySlot()
    {
        if(int.Parse(es.currentSelectedGameObject.name) == UnavailableID)
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

    public void SearchForSameItem(Item item, int count) // дл€ стыковки патронов 
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].id == item.id)
            {
                if (items[0].count < FullAmmo)
                {
                    items[i].count += count;
                    if(items[i].count > FullAmmo)
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
        if(count > 0)
        {
            for(int i = 0; i < maxCount; i++)
            {
                if(items[i].id == 0)
                {
                    AddItem(i,item, count);
                    i = maxCount;
                }
            }
        }
    }
}

public class ItemInventory
{
    public int id;
    public GameObject itemobj;
    public int count;
}