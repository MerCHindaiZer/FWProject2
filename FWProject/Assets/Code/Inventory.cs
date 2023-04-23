using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public partial class Inventory : MonoBehaviour
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
    public  TMP_Text money, Name, Weight, DMG, Resist;
    public int price;
    public int FullAmmo;
}
