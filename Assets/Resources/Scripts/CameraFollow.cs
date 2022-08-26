using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;
    //  public float cameraSize;

    private float smoothMove = 1.5f;

    // Use this for initialization
    void Start()
    {
        // cameraSize = Camera.main.orthographicSize * 0.8f;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothMove * Time.deltaTime);
    }

    public void ShotMove(float movehight)
    {
        Vector3 temp = transform.position;
        temp.y += movehight;
        transform.position = temp;
        targetPos.y = temp.y;
    }
}
