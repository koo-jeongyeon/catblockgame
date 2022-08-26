using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    private int count;
    private int nowcount;//1판에서의 머니
    void Awake()
    {
        count = 0;
        nowcount = 0;
    }

    public void AddCount(int mon)
    {
        count += mon;
        nowcount += mon;
    }

    public int GetMoney()
    {
        return count;
    }
    public int GetNowMoney()
    {
        return nowcount;
    }
    

    public void MoneyUpdate(int mon)
    {
        count = mon;
    }

    void Update()
    {
        transform.GetComponent<Text>().text = count+"";
    }
}
