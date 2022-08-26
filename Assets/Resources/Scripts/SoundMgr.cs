using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public AudioSource audioSource;

    public Button Onbutton;
    public Button Offbutton;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public AudioClip catdown_sound;
    public AudioClip start_sound;
    public AudioClip coin_sound;
    public AudioClip open_sound;
    public AudioClip push_sound;
    public AudioClip gameend_sound;
    public AudioClip catfix_sound;

    public void ButtonSound()
    {
        audioSource.PlayOneShot(push_sound, 0.5f);
    }

    public void OpenSound()
    {
        audioSource.PlayOneShot(open_sound, 0.5f);
    }

    public void PushSound()
    {
        audioSource.PlayOneShot(push_sound, 0.5f);
    }

    public void CatdownSound()
    {
        audioSource.PlayOneShot(catdown_sound, 0.5f);
    }

    public void CoinSound()
    {
        audioSource.PlayOneShot(coin_sound, 0.3f);
    }

    public void CatfixSound()
    {
        audioSource.PlayOneShot(catfix_sound, 0.7f);
    }
    public void GameendSound()
    {
        audioSource.PlayOneShot(gameend_sound, 0.5f);
    }

    public void SoundUpdate(int volume)
    {

        audioSource.volume = volume;
    }

    public void OnVolume()
    {
        audioSource.volume = 1f;
        Onbutton.gameObject.SetActive(true);
        Offbutton.gameObject.SetActive(false);
        GamePlayController.instance.save_Mgr.SaveSound(1);
    }
    public void OffVolume()
    {
        audioSource.volume = 0f;
        Onbutton.gameObject.SetActive(false);
        Offbutton.gameObject.SetActive(true);
        GamePlayController.instance.save_Mgr.SaveSound(0);
    }

}
