using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    /*
     * 슬롯
     * 1. 플레이어가 가지고 있는 아이템은 lock 없애줌
     * 
     */
    [HideInInspector]
    public Item item;

    public Image icon;
    public bool use; //가지고있는 상태인지 아닌지
    public bool aduse; //광고로 얻을수있는 상태인지
    public GameObject lockbg;
    public GameObject ad;

    public bool NotClick;
    
    public void Additem(Item _item)
    {
        item = _item;
        icon.sprite = _item.itemIcon;
    }

    public void Useitem(bool _use)
    {
        use = _use;
        if (use)
        {
            lockbg.SetActive(false);
        }
    }
    public void UseAd(bool _use)
    {
        aduse = _use;
        if (aduse)
        {
            ad.SetActive(true);
        }

    }
    public void OnMouseDown()
    {
        if(!NotClick)
        {
            if (use)
            {
                GamePlayController.instance.SaveCatBox(item);
            }
            else
            {
                if (aduse)
                {
                    if(item.itemName == "왕자고양이" || item.itemName == "장미고양이"
                        || item.itemName == "상자고양이" || item.itemName == "집사고양이")
                    {
                        GamePlayController.instance.BuyPopView(item);
                    }
                    else
                    {
                        GamePlayController.instance.ADpopView(item);
                    }
                }
            }
        }
        

    }

    // Use this for initialization
    void Start () {
        NotClick = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
