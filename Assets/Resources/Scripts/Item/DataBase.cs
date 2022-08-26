using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour {

    public List<Item> itemList = new List<Item>();

	// Use this for initialization
	void Awake () {

        itemList.Add(new Item(0, 0, "기본고양이"));
        itemList.Add(new Item(1, 10, "회색고양이"));
        itemList.Add(new Item(2, 30, "하얀고양이"));
        itemList.Add(new Item(3, 50, "얼룩고양이"));
        itemList.Add(new Item(4, 70, "갈색고양이"));
        itemList.Add(new Item(5, 90, "샴고양이"));
        itemList.Add(new Item(6, 120, "길고양이"));
        itemList.Add(new Item(7, 170, "얼룩고양이"));
        itemList.Add(new Item(8, 220, "야광고양이"));
        itemList.Add(new Item(9, 250, "오드아이고양이"));
        itemList.Add(new Item(10, 300, "액체고양이"));
        itemList.Add(new Item(11, 350, "두목고양이"));
        itemList.Add(new Item(12, 1500, "장미고양이"));
        itemList.Add(new Item(13, 1500, "상자고양이"));
        itemList.Add(new Item(14, 3000, "집사고양이"));
        itemList.Add(new Item(15, 5000, "왕자고양이"));

    }
    

}
