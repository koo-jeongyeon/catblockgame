using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChureBar: MonoBehaviour
{

    public Chure chureScript;
  
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Box")
        {
            chureScript.ChureEat();
        }

    }
}
