using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler , IDragHandler, IEndDragHandler {

    public Item item;
    public int amount;
    public int slot;

  
    private Inventory inv;
    private Vector2 offset;

 void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    public void OnBeginDrag(PointerEventData eventData) // не знаю що це таке якийсь інтерфейс який появився з помощю гарячик кнопок (3)
    {
       if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y); // wtf ?
            this.transform.SetParent(this.transform.parent.parent); // виводим ітем як окрему частинку хз як назвати (ну крч щоб воно чайлдом не було)
            this.transform.position = eventData.position- offset;  // переміщувалось за мишкою 
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset; // тут воно прикорило переміщення за мишкою

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        this.transform.SetParent(inv.slots[slot].transform); //створилось щоб ітем який вийшов за межі інвентаря  ,залишався чайлдом канваза а не зникав 
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;// в інспекторі на предметі ми добавили канваз групу  ,через яку ми не могли переміщувати ,а завдяки цій строкі і (23) можем
    }
}
