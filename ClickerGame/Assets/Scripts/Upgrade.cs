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
    public int startingCost;
    public int currentCost;
    public enum type
    {
        ClickAdd,
        ClickMult,
        AutoAdd,
        AutoMult,
        MoneyAdd,
        RAMAdd,
        RAMMult
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
        currentCost = startingCost;
        gm = GameObject.Find("WorldCanvas").GetComponent<GameManager>();
        disName.text = nam;
        disDesc.text = desc;
        if (isProject)
        {
            disCost.text = currentCost.ToString();
            disProgress.gameObject.SetActive(true);
            disProgressText.text = timeToFinish.ToString();
        }
        else
        {
            disCost.text = "$" + currentCost.ToString();
            disProgress.gameObject.SetActive(false);
        }
        disButton.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        if (isProject && (gm.nm.disNum >= currentCost && gm.nm.disNumAbb >= 1) && !projectCountdown)
        {
            gm.nm.Sub(currentCost, 1);
            projectCountdown = true;
            curTTF = timeToFinish;
        }
        else if (!isProject && gm.money >= currentCost)
        {
            gm.nm.Sub(currentCost, 1);
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
            gm.ClickMult *= upgradeValue;
        }
        else if (upgradeType == type.AutoAdd)
        {
            gm.AutoClick += upgradeValue;
        }
        else if (upgradeType == type.AutoMult)
        {
            gm.AutoMult *= upgradeValue;
        }
        else if (upgradeType == type.MoneyAdd)
        {
            gm.money += upgradeValue;
        }
        else if (upgradeType == type.RAMAdd)
        {
            gm.RAMSpeed += upgradeValue;
        }
        else if (upgradeType == type.RAMMult)
        {
            gm.RAMSpeed *= upgradeValue;
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
            currentCost = startingCost * Mathf.FloorToInt(Mathf.Pow(timesPurchased+1, 2)); //TBD
            disCost.text = "$" + currentCost.ToString();
        }
        if (isProject)
        {
            disCost.text = currentCost.ToString();
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
            curTTF -= Time.deltaTime * gm.RAMSpeed;
            disProgressText.text = curTTF.ToString("F0");
            if (curTTF <= 0)
            {
                BuySuccess();
                projectCountdown = false;
            }
            
        }
    }

    public void Reload()
    {
        if (timesPurchased >= 1)
        {
            if (upgradeKind == quantity.Single)
            {
                gameObject.SetActive(false);
            }
            else if (upgradeKind == quantity.Repeatable)
            {
                currentCost = startingCost * Mathf.FloorToInt(Mathf.Pow(timesPurchased + 1, 2)); //TBD
                disCost.text = "$" + currentCost.ToString();
            }
            if (isProject)
            {
                disCost.text = currentCost.ToString();
                disProgressText.text = timeToFinish.ToString();
                disProgress.fillAmount = 1f;
            }
        }
        
    }
}
