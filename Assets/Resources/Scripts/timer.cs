using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private float time;
    public Text timeText;

    private bool timeout;
    // Start is called before the first frame update
    void Awake()
    {
        transform.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        time = -1;
        timeout = false;
    }
    public void TimerPlay()
    {
        transform.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        time = 5;
    }
    //드랍이 되면 타이머도 멈춤
    public void TimerStop()
    {
        transform.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        time = -1;
    }
    //시간다 지났는데 드랍안되면? 강제드랍 게임종료


    //box_Obj.GetComponent<BoxScript>().canMove && 
    // Update is called once per frame
    void Update()
    {
        if(timeout != true)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                timeText.text = Mathf.Ceil(time).ToString();
            }

            if (0 > time && time > -1)
            {
                timeout = true;
                GamePlayController.instance.TimeOut();
            }
        }
        
    }
}
