using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

    public static GamePlayController instance;

    public BoxSpawner box_Spawner;

    public Inventory inventory;
        
    [HideInInspector]
    public BoxScript currentBox;
    [HideInInspector]
    public float moveCamera;
    [HideInInspector]
    public bool listck;
    [HideInInspector]
    public float moveGameover;

    public CameraFollow cameraScript;

    public GainText gainText;
    public MoneyText moneyText;

    public GameObject buttonUI;

    [HideInInspector]
    public Sprite catitem;
    [HideInInspector]
    public Item Savecatitem;
    [HideInInspector]
    public List<GameObject> boxlist;
    [HideInInspector]
    public bool boxfix;

    public GameObject box;

    public SaveMgr save_Mgr;

    public GameObject ADpop;
    public Image ADImage;
    public GameObject Buypop;
    public Image BuyImage;
    public Text Buyprice;
    public AdmobAdManager admob_Mgr;

    public upanime upanime;

    public SoundMgr sound_Mgr;
    public GameOver gameoverScript;
    public timer timerScript;
    public Chure chureScript;
    //private int moveCount;
    public GameObject Restart;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
        moveCamera = 7f;
        moveGameover = -4.18f;
        boxlist = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        DetectInput();

        if (admob_Mgr.reward)
        {
            admob_Mgr.reward = false;
            int itemID = Savecatitem.itemID;
            save_Mgr.SaveUseCatID(itemID);
            InvenUseCatUpdate(save_Mgr.useCatID);

            ADpop.SetActive(false);
            inventory.RemoveSlot();
            InvenToryView();
        }
        
    }

    public void boxFixing()
    {
        foreach(GameObject it in boxlist)
        {
            it.GetComponent<BoxScript>().StarticChange();
        }

    }
    
    public void ADpopView(Item _item)
    {
        ADpop.SetActive(true);
        ADImage.sprite = _item.itemIcon;

        Vector2 temp = new Vector2(550, 250);
        if (_item.itemName == "왕자고양이")
        {
            temp.y = 350;
        }else if(_item.itemName == "상자고양이")
        {
            temp.y = 340;
        }

        ADImage.rectTransform.sizeDelta = temp;
        //save_Mgr.SaveReward(_item.itemID);
        Savecatitem = _item;

        sound_Mgr.OpenSound();

        inventory.NotClickMode(true);

    }
    public void BuyPopView(Item _item)
    {
        Buypop.SetActive(true);
        BuyImage.sprite = _item.itemIcon;
        Buyprice.text = _item.price+"";

        Vector2 temp = new Vector2(550, 250);
        if (_item.itemName == "왕자고양이")
        {
            temp.y = 350;
        }
        else if (_item.itemName == "상자고양이")
        {
            temp.y = 340;
        }
        else if (_item.itemName == "집사고양이")
        {
            temp.y = 287;
        }

        BuyImage.rectTransform.sizeDelta = temp;
        Savecatitem = _item;
        sound_Mgr.OpenSound();

        inventory.NotClickMode(true);
    }

    public void BuyBox()
    {
        int mymon = moneyText.GetMoney();
        int itemmon = Savecatitem.price;
        if (mymon >= itemmon)
        {
            int itemID = Savecatitem.itemID;
            save_Mgr.SaveUseCatID(itemID);
            InvenUseCatUpdate(save_Mgr.useCatID);

            Buypop.SetActive(false);
            inventory.RemoveSlot();
            InvenToryView();

            int money = mymon - itemmon;
            SaveMoney(money);

        }
    }

    public void ADpopXbtn()
    {
        ADpop.SetActive(false);
        sound_Mgr.ButtonSound();
        inventory.NotClickMode(false);
    }

    public void BuypopXbtn()
    {
        Buypop.SetActive(false);
        sound_Mgr.ButtonSound();
        inventory.NotClickMode(false);
    }

    public void UserADview()
    {
         admob_Mgr.UserChoseToWatchAd();
    }
    
    public void GameStart() //시작버튼
    {
        Item item = inventory.GetItem(catitem);
        box_Spawner.SpawnBox(item);
        box_Spawner.CatBoxChange(catitem);
        buttonUI.SetActive(false);

        sound_Mgr.ButtonSound();
    }

    public void ItemListBtn() //리스트버튼
    {
        if (listck != true)
        {
            cameraScript.ShotMove(-9.75f);
            
            InvenToryView();
            //Invoke("InvenToryView", 1.3f);
            listck = true;
        }

        sound_Mgr.OpenSound();
    }

    public void InventoryXbtn() //인벤토리 창닫기
    { 
        inventory.RemoveSlot();
        inventory.gameObject.SetActive(false);
        ADpop.SetActive(false);
        buttonUI.SetActive(true);
        cameraScript.ShotMove(9.75f);
        listck = false;


        sound_Mgr.ButtonSound();
    }

    public void ChureShow()
    {
        chureScript.ChureStart();

    }

    public void SpecialBox(Item _item,GameObject box)
    {
        //스페셜고양이 콜라이더조절
        Vector2 size = new Vector2(4.48f, 1.99f);
        Vector2 offset = new Vector2(0f, 0f);
        if (_item.itemName == "왕자고양이")
        {
            size.y = 1.89f;
            offset.y = -0.6f;
        }else if(_item.itemName == "상자고양이")
        {
            size.y = 2f;
            offset.y = -0.22f;
        }
        else if (_item.itemName == "집사고양이")
        {
            size.y = 2f;
            offset.y = -0.22f;
        }
        box.GetComponent<BoxCollider2D>().size = size;
        box.GetComponent<BoxCollider2D>().offset = offset;
    }

    public void SaveCatBox(Item _item)
    {
        save_Mgr.SaveCatID(_item.itemID);
        catitem = _item.itemIcon;
        //기본 박스바꿈
        box.GetComponent<SpriteRenderer>().sprite = catitem;

        SpecialBox(_item, box);

        sound_Mgr.ButtonSound();
    }
    public void Setcatitem(int catID)
    {
        Item item = inventory.GetItem(catID);
        catitem = item.itemIcon;

        //기본 박스바꿈
        box.GetComponent<SpriteRenderer>().sprite = catitem;

        SpecialBox(item, box);

    }

    public void InvenToryView()
    {
        inventory.gameObject.SetActive(true);
        inventory.InvenitemList();
        inventory.SoltCreate();
        inventory.SlotUse();
        inventory.SlotAdUse();
    }

    public void InvenUseCatUpdate(string usecat)
    {
        inventory.useCatUpdate(usecat);
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();

        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 1f);
    }
    public void NewBox()
    {
        Item item = inventory.GetItem(catitem);
        box_Spawner.SpawnBox(item);
        box_Spawner.CatBoxChange(catitem);
    }

    public void AClear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void TimerPlay()
    {
        timerScript.TimerPlay();
    }
    public void TimerStop()
    {
        timerScript.TimerStop();
    }
    public void TimeOut()
    {
        Destroy(currentBox.gameObject);
        RestartGame();
    }

    public void MoveCamera()
    {
        GameObject box = boxlist[boxlist.Count - 1];
        
        float box_y = box.transform.position.y;
        float move = box_y + moveCamera;
        
        cameraScript.targetPos.y = move;
    }

    public void GameOverMove()
    {
        GameObject box = boxlist[boxlist.Count - 1];

        float box_y = box.transform.position.y;
        float move = box_y + moveGameover;

        gameoverScript.GameOverMove(move);
    }
    public void SoundUpdate(int volume)
    {
        sound_Mgr.SoundUpdate(volume);
    }
    public void MoneyUpdate(int mon)
    {
        moneyText.MoneyUpdate(mon);
    }
    public void UpdateGain()
    {
        gainText.AddCount();
    }
    public void UpdateMoney(int mon)
    {
        moneyText.AddCount(mon);
    }
    public void SaveMoney(int mon)
    {
        save_Mgr.SaveMoney(mon);
    }
 
    public void RestartPopup()
    {
        Restart.SetActive(true);
        int money = moneyText.GetNowMoney();
        int gain = gainText.Getgain();
        Restart.transform.Find("score").GetComponent<Text>().text = gain+"";
        Restart.transform.Find("money").GetComponent<Text>().text = money+"";
    }
    public void RestartBtn()
    {
        //처음부터 시작
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //카운트 리셋
        gainText.ResetCount();
        //박스 리셋
        boxlist.Clear();
        Restart.SetActive(false);
    }

    public void RestartGame()
    {
        //코인저장
        int money = moneyText.GetMoney();
        SaveMoney(money);
        Destroy(currentBox.gameObject);
        //재시작창띄우기
        RestartPopup();

    }


}
