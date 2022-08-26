using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void GameOverMove(float move)
    {
        Vector3 temp = transform.position;
        temp.y = move;
        transform.position = temp;
        targetPos.y = temp.y;
    }

}
