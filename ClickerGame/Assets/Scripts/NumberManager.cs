using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public string[] abb;
    
    
    public int disNum;
    public int disNumAbb;

    public int overNum;

    GameManager gm;



    public string resourceType;


    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
        abb = new string[]
        {
             "", ".", "T", "M", "B", "T", "Q", "Q"
        };
        disNumAbb = 1;
    }

    // Update is called once per frame
    void Update()
    {
        gm.displayPts.text = disNum + abb[disNumAbb] + overNum.ToString("000") + abb[disNumAbb - 1] + " " + resourceType;
    }

    public string FormatForDisplay(int num, int numAbb)
    {
        string formatted = num + abb[numAbb];
        return formatted;
    }

    public void Add(int NumToAdd, int AbbOfNumToAdd)
    {
        if (AbbOfNumToAdd == disNumAbb)
        {
            disNum += NumToAdd;
        }
        else if (AbbOfNumToAdd == disNumAbb - 1)
        {
            overNum += NumToAdd;
        }
        while (overNum.ToString().Length > 3)
        {
            disNum += 1;
            overNum -= 1000;
        }
        if (disNum.ToString().Length > 3)
        {
            overNum = int.Parse(disNum.ToString().Substring(disNum.ToString().Length - 3));
            disNum /= 1000;
            disNumAbb++;
        }
    }

    public void Sub(int NumToSub, int AbbOfNumToSub)
    {
        if (AbbOfNumToSub == disNumAbb)
        {
            disNum -= NumToSub;
        }
        else if (AbbOfNumToSub == disNumAbb - 1)
        {
            overNum -= NumToSub;
        }
        if (disNum < 0)
        {
            disNumAbb -= 1;
            overNum -= disNum;
            disNum = overNum;
            overNum = 0;
        }
       
    }

}
