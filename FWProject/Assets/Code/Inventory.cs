using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase Data; // ��������� ������ � �����, ID, ������� � ������������

    public List<ItemInventory> items = new List<ItemInventory>();

    public int UnavailableID; // ID ����������� �����

    public int maxCount; // ����� ������

    public int currentID; // ID ������

    public EventSystem es;

}
