using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSpawner : MonoBehaviour {

    public GameObject box_Prefab;
    

    [HideInInspector]
    public GameObject box_Obj;
    private void Start()
    {
      //  SpawnBox();
    }

    //박스생겼을때 부터 타임재기 5초 
    //5초안에 드랍안하면 박스드랍되고 게임오버

    public void SpawnBox(Item _item)
    {
        box_Obj = Instantiate(box_Prefab);

        Vector3 temp = transform.position;
        temp.z = 0;
        box_Obj.transform.position = temp;
        GamePlayController.instance.SpecialBox(_item, box_Obj);
        GamePlayController.instance.boxlist.Add(box_Obj);
        GamePlayController.instance.TimerPlay();
    }

    public void CatBoxChange(Sprite icon)
    {
        box_Obj.GetComponent<SpriteRenderer>().sprite = icon;
    }

    private void Update()
    {
        
    }



}
