using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {

    private List<Item> database = new List<Item>(); // приватний для того щоб ліст ітемів бачили тіки у межах цього скріпта , (створюєм лист ітемів)
    private JsonData itemData; // не знаю ,що робий це плагін  

    private void Start()
    {
       //   Item item = new Item(0, "Ball" , 5 );// воно бере копіює дані з класу Ітем
       // database.Add(item); // добавляєм ітек  ??? або я хз
       //  Debug.Log(database[0].Title); // виводим в консольку ід і назву ітема ,перевіряєм чи працює
        // він не провіривши убрав цих три рядка ,бо має кращу ідею як це реалізувати


        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); //штука через яку ми будем вказувати ітеми через плагін Джейсон
        ConstructItemDatabase();


        Debug.Log(FetchItemByID(0).Description); // вивели для перевірки назви ітему

 }
    
    
     public Item FetchItemByID(int id) // don`t understand wtf is that 
    {
        for (int i=0; i<database.Count; i++) // зробили  функцію окрему яка шукає ід ітема
        
            if (database[i].ID == id)
             return database[i];
        
        return null;
    }




    void ConstructItemDatabase()
    {
        for (int i = 0; i< itemData.Count; i++ )
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["power"], (int)itemData[i]["stats"]["defence"], 
                (int)itemData[i]["stats"]["vitality"], itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()
                                         )); // строка яка реалізовує 3 строчки які закоменчені
        }


    }


}

public class Item
{
    public int ID { get; set; } // знаходитемо ід ітемі ,які ми позначили через плагін джейсон
    public string Title { get; set;  } //  воно появиться в інспекторі де ми маєм вказувати назву ітема якось так
    public int Value { get; set; } // так само як і попередні 
    public int Power { get; set; }
    public  int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set;  }

    public Item (int id, string title , int value, int power ,int defence, int vitality, string description ,bool stackable ,int rarity ,string slug) // зробили окрему функції (хз як назвати ) де будем виконувати вище вказані дії
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Item/" + slug);
         

    }

    public Item()
    {
        this.ID = -1;
    }
  
}
