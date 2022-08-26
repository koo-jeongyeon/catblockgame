using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftanime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        leftanimeEnd();
    }

    public void leftanimeStart()
    {
        transform.gameObject.SetActive(true);
        Animation anime = transform.GetComponent<Animation>();
        anime.Play();
        GamePlayController.instance.sound_Mgr.CoinSound();
    }

    public void leftanimeEnd()
    {
        transform.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
