using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Text disName;
    public Text disDesc;
    public Text disCost;
    public Button disButton;

    public string nam;
    public string desc;
    public int cost;
    public enum type
    {
        ClickAdd,
        ClickMult,
        AutoAdd,
        AutoMult,
        MoneyAdd
    }
    public type upgradeType;
    public int upgradeValue;
    public bool ShowThing;
    public GameObject ThingToShow;
    
    public enum quantity
    {
        Repeatable,
        Single,
        Static
    }
    public quantity upgradeKind;

    public bool isProject;
    public int timeToFinish;
    
    public int peek;


    // Start is called before the first frame update
    void Start()
    {
        disName.text = nam;
        disDesc.text = desc;
        if (isProject)
        {
            disCost.text = cost.ToString();
        }
        else
        {
            disCost.text = "$" + cost.ToString();
        }
        disButton.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        bool successfulPurchase = false;
        if (isProject && GameManager.pts >= cost)
        {
            GameManager.pts -= cost;
            successfulPurchase = true;
        }
        else if (!isProject && GameManager.money >= cost)
        {
            GameManager.money -= cost;
            successfulPurchase = true;
        }

        if (successfulPurchase)
        {
            if (upgradeType == type.ClickAdd)
            {
                GameManager.ClickAmount += upgradeValue;
            }
            else if (upgradeType == type.ClickMult)
            {
                GameManager.ClickAmount *= upgradeValue;
            }
            else if (upgradeType == type.AutoAdd)
            {
                GameManager.AutoClick += upgradeValue;
            }
            else if (upgradeType == type.AutoMult)
            {
                GameManager.AutoClick *= upgradeValue;
            }
            else if (upgradeType == type.MoneyAdd)
            {
                GameManager.money += upgradeValue;
            }
            if (ShowThing)
            {
                ThingToShow.SetActive(true);
            }
            if (upgradeKind == quantity.Single)
            {
                Destroy(gameObject);
            }
            else if (upgradeKind == quantity.Repeatable)
            {
                float newCost = cost * 1.5f;
                cost = Mathf.RoundToInt(newCost);
                disCost.text = cost.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pts >= peek)
        {
            //TBD
        }
    }
}
