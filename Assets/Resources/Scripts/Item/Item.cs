using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int itemID;
    public string itemName;
    public int price;
    public Sprite itemIcon; //아이콘

    public Item(int _itemID, int _price, string _itemName)
    {
        itemID = _itemID;
        itemName = _itemName;
        price = _price;
        itemIcon = Resources.Load("ItemIcon/" + itemID.ToString(), typeof(Sprite)) as Sprite;
        //sprite 로 가져옴 typeof 는 선언만 하는것이고 as 로 실제 sprite 로 가져오는것
    }
}
