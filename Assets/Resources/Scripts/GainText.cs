using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainText : MonoBehaviour {

    private int count;
	// Use this for initialization
	void Start () {
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<Text>().text = count+"";

    }

    public void AddCount()
    {
        count++;
    }
    public void ResetCount()
    {
        count = 0;
    }

    public int Getgain()
    {
        return count;
    }
}
