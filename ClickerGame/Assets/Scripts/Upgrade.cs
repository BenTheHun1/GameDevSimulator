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
    public Image disProgress;
    public Text disProgressText;
    GameManager gm;
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

    public int timesPurchased;

    public enum quantity
    {
        Repeatable,
        Single,
        Static
    }
    public quantity upgradeKind;

    public bool isProject;
    public float timeToFinish;

    bool projectCountdown;
    float curTTF;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("WorldCanvas").GetComponent<GameManager>();
        disName.text = nam;
        disDesc.text = desc;
        if (isProject)
        {
            disCost.text = cost.ToString();
            disProgress.gameObject.SetActive(true);
            disProgressText.text = timeToFinish.ToString();
        }
        else
        {
            disCost.text = "$" + cost.ToString();
            disProgress.gameObject.SetActive(false);
        }
        disButton.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        if (isProject && gm.pts >= cost && !projectCountdown)
        {
            gm.pts -= cost;
            projectCountdown = true;
            curTTF = timeToFinish;
        }
        else if (!isProject && gm.money >= cost)
        {
            gm.money -= cost;
            BuySuccess();
        }
    }

    void BuySuccess()
    {
        if (upgradeType == type.ClickAdd)
        {
            gm.ClickAmount += upgradeValue;
        }
        else if (upgradeType == type.ClickMult)
        {
            gm.ClickAmount *= upgradeValue;
        }
        else if (upgradeType == type.AutoAdd)
        {
            gm.AutoClick += upgradeValue;
        }
        else if (upgradeType == type.AutoMult)
        {
            gm.AutoClick *= upgradeValue;
        }
        else if (upgradeType == type.MoneyAdd)
        {
            gm.money += upgradeValue;
        }
        if (ShowThing)
        {
            ThingToShow.SetActive(true);
        }
        timesPurchased++;
        if (upgradeKind == quantity.Single)
        {
            gameObject.SetActive(false);
        }
        else if (upgradeKind == quantity.Repeatable)
        {
            float newCost = cost * (timesPurchased^2); //TBD
            cost = Mathf.RoundToInt(newCost);
            disCost.text = "$" + cost.ToString();
        }
        if (isProject)
        {
            disCost.text = cost.ToString();
            disProgressText.text = timeToFinish.ToString();
            disProgress.fillAmount = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (projectCountdown)
        {
            disProgress.fillAmount = curTTF / timeToFinish;
            curTTF -= Time.deltaTime;
            disProgressText.text = curTTF.ToString("F0");
            if (curTTF <= 0)
            {
                BuySuccess();
                projectCountdown = false;
            }
            
        }
    }
}
