using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


   [SerializeField] GameObject inventoryPanel; // робим їх ігровими об'єктами щоб в інспекторі можна було їх вказати  (всі 4)
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount; // змінна скільки слотів ми створюєм 
    public List<Item> items = new List<Item>(); // створюєм ще один лист для ітемів і окремо лист для слотів що можна було їх опріділяти (так само знизу)
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 16;
       // inventoryPanel = GameObject.Find("Inventory Panel"); // знаходим інвентарню панель 
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; i++)  // цикл для слотів 
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot, slotPanel.transform)); // тут ми створюєм слот 
            slots[i].GetComponent<Slot>().id = i; // i don`t know what i do
            slots[i].GetComponent<Slot>().inv = this;
           // slots[i].transform.SetParent(slotPanel.transform); // а тут ми переміщуєм його сюди

        }

        AddItem(0);
        AddItem(1);
         AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);

        Debug.Log(items[1].Title);
    }


    public void AddItem(int id) // функція для находження вільного слота 
    {


        Item itemToAdd = database.FetchItemByID(id); //підключили функцію зі скрипта дата бейз для пошуку ід  ітемя
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;

                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {

                if (items[i].ID == -1) // виконуєм умову що якщо ід ітема - 1 то виконується все що нижче 
                {
                    items[i] = itemToAdd; //добавляєм ітем у вільний слот 
                    GameObject itemObject = Instantiate(inventoryItem); //інстантіюємо його 
                    itemObject.GetComponent<ItemData>().item = itemToAdd;
                    itemObject.GetComponent<ItemData>().slot = i;
                    itemObject.transform.SetParent(slots[i].transform); // отримує знаходженням вільного слота 
                    itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObject.transform.position = Vector2.zero; //замінюєм його на наш ітем 
                    itemObject.name = itemToAdd.Title;
                    break; //виходим
                }
            }

        }
    }
     
    bool CheckIfItemIsInInventory (Item item)
    {
        for (int i = 0; i< items.Count;i++)
        
            if (items[i].ID == item.ID)
                return true;
            return false; 
        
    }

}
