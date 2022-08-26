using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    private float min_x = -4.5f, max_x = 4.5f;
    private float currentPosition; //현재위치
    private float move_Speed; //이동속도

    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public float boxhight; //박스높이

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;
    

    private int fixCount;
    private int cameraCount;
    

    // Use this for initialization

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;
    }
    void Start () {
        canMove = true;
        boxhight = 250;
        move_Speed = Random.Range(12, 18);
        fixCount = 12;
        cameraCount = 3;

        if (Random.Range(0, 2) > 0)
        {
            move_Speed *= -1f;
        }

        currentPosition = transform.position.x;
        GamePlayController.instance.currentBox = this;
	}
	
	// Update is called once per frame
	void Update () {
        MoveBox();
        UseEnd();
    }

    void UseEnd()
    {
        if(ignoreCollision && ignoreTrigger)
        {
            myBody.gravityScale = 0;
        }
    }

    void MoveBox()
    {
        if (canMove)
        {
            currentPosition += move_Speed * Time.deltaTime;

            if(currentPosition >= max_x)
            {
                move_Speed *= -1f;
                currentPosition = max_x;
            }
            else if(currentPosition <= min_x)
            {
                move_Speed *= -1f;
                currentPosition = min_x;
            }
            Vector3 temp = transform.position;
            temp.x = currentPosition;
            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = 13;//Random.Range(10, 10);
        GamePlayController.instance.TimerStop();
        GamePlayController.instance.sound_Mgr.PushSound();
    }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        //ignoreTrigger = true;
        
        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.GameOverMove();


    }

    public void StarticChange()
    {
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void RestartGame()
    {
        GamePlayController.instance.RestartGame();
    }

    //박스와 부딫혔을때 (=박스를 쌓았을때)
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;
        
        if (target.gameObject.tag == "Platform" || target.gameObject.tag == "Box")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
            
            //애니메이션
            GamePlayController.instance.upanime.upanimeStart();
            Animation anime = transform.GetComponent<Animation>();
            anime.Play();

            ////y조정
            float target_pos_y = target.transform.position.y;
            float aim_pos_y = target_pos_y + 2;
            float my_pos_y = transform.position.y;
            if (my_pos_y < aim_pos_y)
            {
                Vector3 temp = transform.position;
                temp.y = aim_pos_y;
                transform.position = temp;
            }

            int count = GamePlayController.instance.boxlist.Count;
            if (count % fixCount == 0)
            {
                GamePlayController.instance.boxFixing();
                GamePlayController.instance.sound_Mgr.CatfixSound();
            }
            if (count % cameraCount == 0)
            {
                GamePlayController.instance.MoveCamera();
                GamePlayController.instance.ChureShow();
            }


            GamePlayController.instance.sound_Mgr.CatdownSound();
        }

        GamePlayController.instance.UpdateGain();
        GamePlayController.instance.UpdateMoney(1);

        //타겟이 게임오버 되지않음 플랫폼엔 이 스크립트가 없으니까 맨뒤에실행해야함
        target.gameObject.GetComponent<BoxScript>().ignoreTrigger = true;
    }
    

    private void OnTriggerEnter2D(Collider2D target)
    {
        
        if(target.tag == "gameover")
        {
            if (ignoreTrigger)
                return;

            GameOver();
            
        }

        if(target.tag == "mustgameover")
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        CancelInvoke("Landed");
        gameOver = true;
        ignoreTrigger = true;

        Invoke("RestartGame", 1f);
    }
}
