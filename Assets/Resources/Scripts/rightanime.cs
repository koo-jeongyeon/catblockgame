using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightanime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        rightanimeEnd();
    }

    public void rightanimeStart()
    {
        transform.gameObject.SetActive(true);
        Animation anime = transform.GetComponent<Animation>();
        anime.Play();
        GamePlayController.instance.sound_Mgr.CoinSound();
    }

    public void rightanimeEnd()
    {
        transform.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
