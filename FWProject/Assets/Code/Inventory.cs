using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase Data; // хранилище данных о вещах, ID, спрайты и наименовани€

    public List<ItemInventory> items = new List<ItemInventory>();

    public int UnavailableID; // ID недоступных €чеек

    public int maxCount; // число слотов

    public int currentID; // ID €чейки

    public EventSystem es;

}
