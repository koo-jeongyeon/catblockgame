using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chure : MonoBehaviour
{
    public GameObject chureLeft;
    public GameObject chureRight;

    public leftanime leftanime;
    public rightanime rightanime;

    [HideInInspector]
    public Vector3 targetPos;
    
    private GameObject ChooseChure;
    private float smoothMove = 1.5f;

    private int Money;
    private bool chureStart;
    private float aim;

    private float time;

    

    // Start is called before the first frame update
    void Start()
    {
        Money = 10;
        time = 0;
    }
    /*
     * 정해져야 하는것 
     * 1. y위치 : -1.7 시작위치고정(카메라랑 같이 움직임)
     * 2. x위치 : 5~7 정도 랜덤
     * 3. 왼쪽인지(왼쪽이면 -해줌) 오른쪽인지 
     */
    // Update is called once per frame

    public void ChureStart()
    {
        float check = Random.Range(0, 10);
        if(check > 5)
        {
            chureStart = true;

            switch (check)
            {
                case 6: aim = 4.79f; break;
                case 7: aim = 6.32f; break;
                case 8: aim = 4.79f;  break;
                case 9: aim = 5.79f;  break;
                case 10: aim = 4.79f;  break;
            }
            float choose = Random.Range(0, 2);
            if (choose == 0)
            {
                ChooseChure = chureLeft;
                targetPos = chureLeft.transform.position;
                targetPos.x = -aim;
            }
            else
            {
                ChooseChure = chureRight;
                targetPos = chureRight.transform.position;
                targetPos.x = aim;
            }

            time = 10;
        }

    }

    public void ChureEnd()
    {
        chureStart = false;
        time = 0;
        float _x = 0f;
        if (ChooseChure == chureLeft)
            _x = -10;
        else
            _x = 10;

        ChooseChure.transform.position = new Vector2(_x, ChooseChure.transform.position.y);
        ChooseChure = null;
    }
    public void ChureEat()
    {
        GamePlayController.instance.UpdateMoney(10);
        if (ChooseChure == chureLeft)
            leftanime.leftanimeStart();
        else
            rightanime.rightanimeStart();

        ChureEnd();


    }

        void Update()
    {
        if (chureStart)
        {
            Vector2 temp = new Vector2(targetPos.x, ChooseChure.transform.position.y);
            ChooseChure.transform.position = Vector2.Lerp
                (ChooseChure.transform.position, temp, smoothMove * Time.deltaTime);

            time -= Time.deltaTime;

            if (time < 0)
                ChureEnd();
        }
        
    }
}
