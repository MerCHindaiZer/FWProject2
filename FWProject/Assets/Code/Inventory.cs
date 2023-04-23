using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public partial class Inventory : MonoBehaviour
{
    public DataBase Data; // ��������� ������ � �����, ID, ������� � ������������
    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjShow, InventoryMainObj; // ��� ��������� ���������

    public int maxCount; // ����� ������

    public int currentID; // ID ������

    public int UnavailableID; // ID ����������� �����

    public ItemInventory currentItem;

    public EventSystem es;

    public RectTransform movingObject;
    public Vector3 offset;
    public Camera cam; // ��� ���������� �������� �� ��������

    public int florens; //������
    public  TMP_Text money, Name, Weight, DMG, Resist;
    public int price;
    public int FullAmmo;
}
