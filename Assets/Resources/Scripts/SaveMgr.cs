using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMgr : MonoBehaviour
{

    [HideInInspector]
    public int catID;
    [HideInInspector]
    public bool IsSave_catID;
    [HideInInspector]
    public string useCatID;
    [HideInInspector]
    public bool IsSave_useCatID;
    [HideInInspector]
    public int money;
    [HideInInspector]
    public bool IsSave_money;
    [HideInInspector]
    public int sound;
    [HideInInspector]
    public bool IsSave_sound;

    public void SaveCatID(int id)
    {
        catID = id;
        PlayerPrefs.SetInt("catID", catID);
    }
    
    public void SaveUseCatID(int id)
    {
        useCatIDCheck();
        useCatID += "," + id;
        PlayerPrefs.SetString("useCatID", useCatID);
    }

    public void SaveMoney(int mon)
    {
        moneyCheck();
        money = mon;
        PlayerPrefs.SetInt("money", money);
        GamePlayController.instance.MoneyUpdate(money);
    }
    public void SaveSound(int volume)
    {
        sound = volume;
        PlayerPrefs.SetInt("sound", sound);
    }
   

    // Start is called before the first frame update
    void Start()
    {
        IsSave_catID = PlayerPrefs.HasKey("catID");
        if (!IsSave_catID)
        {
            catID = 0;
        }
        else
        {
            catID = PlayerPrefs.GetInt("catID");
            Item it = GamePlayController.instance.inventory.GetItem(catID);
            GamePlayController.instance.SaveCatBox(it);
        }
        GamePlayController.instance.Setcatitem(catID);

        useCatIDCheck();

        moneyCheck();

        soundCheck();
    }
    public void soundCheck()
    {
        IsSave_sound = PlayerPrefs.HasKey("sound");
        if (!IsSave_sound)
        {
            sound = 1;
        }
        else
        {
            sound = PlayerPrefs.GetInt("sound");
        }
        GamePlayController.instance.SoundUpdate(sound);
    }

    public void moneyCheck()
    {
        IsSave_money = PlayerPrefs.HasKey("money");
        if (!IsSave_money)
        {
            money = 0;
        }
        else
        {
            money = PlayerPrefs.GetInt("money");
        }
        GamePlayController.instance.MoneyUpdate(money);
    }

    public void useCatIDCheck()
    {
        IsSave_useCatID = PlayerPrefs.HasKey("useCatID");
        if (!IsSave_useCatID)
        {
            useCatID = "0";
        }
        else
        {
            useCatID = PlayerPrefs.GetString("useCatID");
        }
        GamePlayController.instance.InvenUseCatUpdate(useCatID);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
