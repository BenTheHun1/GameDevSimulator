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
             "", ".", "Tho", "Mil", "Bil", "Tri", "Qua", "Qui"
        };
        disNumAbb = 1;
        resourceType = "Codes";
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log(overNum);
            disNum /= 1000;
            disNumAbb++;
        }
        gm.displayPts.text = disNum + abb[disNumAbb] + overNum.ToString("000") + abb[disNumAbb - 1] + " " + resourceType;
    }

}
