using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public DataBase m_dbmgr;
    private List<Item> inventoryItemList;
    public GameObject slot_Prefab;
    public GameObject ItemView;
    private List<GameObject> slotList;

    private string useCatID;

    private int NowPage;
    private int start=0;
    private int end=1;
    private int slotcount=15;

    // Use this for initialization
    void Awake () {
        inventoryItemList = new List<Item>();
        slotList = new List<GameObject>();
        NowPage = start;
        
    }

    public void useCatUpdate(string usecat)
    {
        useCatID = usecat;
    }
	
    public void InvenitemList() //현재페이지만큼 리스트에 담김
    {
        if(inventoryItemList.Count > 0)
            inventoryItemList.Clear();

        int start = NowPage * 16; //시작
        int end = start + slotcount;//끝
        int listcnt = m_dbmgr.itemList.Count;
        
        if(end+1 >= listcnt)
        {
            end = listcnt;
        }
        List<Item> list = m_dbmgr.itemList.GetRange(start, end);
        
        foreach(Item it in list)
        {
            inventoryItemList.Add(it);
        }
    }
    //리스트만큼 슬롯 복사해주기
    public void SoltCreate()
    {
        slotList.Clear();

        foreach (Item it in inventoryItemList)
        {

            GameObject slot_obj = Instantiate(slot_Prefab);
            slot_obj.transform.SetParent(ItemView.transform);
            Vector3 temp = new Vector3(1, 1, 1);
            slot_obj.transform.localScale = temp;
            slot_obj.GetComponent<Slot>().Additem(it);

            slotList.Add(slot_obj);
        }
    }

    //유저가 아이디를 가지고 있으면 true 인걸로 바꿔주기
    public void SlotUse()
    {
        string[] result = useCatID.Split(new char[] {','});

        for (int i = 0; i < result.Length; i++)
        {
            foreach (GameObject it in slotList)
            {
                if (result[i] == it.GetComponent<Slot>().item.itemID + "" || it.GetComponent<Slot>().item.itemID == 0)
                {
                    it.GetComponent<Slot>().Useitem(true);
                }

            }

        }
    }

    public void SlotAdUse()
    {
        int money = GamePlayController.instance.save_Mgr.money;

        foreach (GameObject it in slotList)
        {
            Slot slot = it.GetComponent<Slot>();
            if (!slot.use)
            {
                int itemprice = slot.item.price;

                if(itemprice <= money || slot.item.itemName == "왕자고양이" || slot.item.itemName == "장미고양이"
                        || slot.item.itemName == "상자고양이" || slot.item.itemName == "집사고양이")
                {
                    slot.UseAd(true);

                }

            }
        }
    }

    public void NotClickMode(bool mode)
    {
        foreach (GameObject it in slotList)
        {
            it.GetComponent<Slot>().NotClick = mode;
        }
    }

    public void RemoveSlot()
    {
        foreach (GameObject it in slotList)
        {
            Destroy(it);
        }
    }

    public Item GetItem(int id)
    {
        foreach(Item it in m_dbmgr.itemList)
        {
           if(it.itemID == id)
            {
                return it;
            }
        }
        return null;
    }

    public Item GetItem(Sprite sprite)
    {
        foreach (Item it in m_dbmgr.itemList)
        {
            if (it.itemIcon == sprite)
            {
                return it;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
