using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float pts;
    public float money;
    public int ClickAmount;
    public int ClickMult;
    public int AutoClick;
    public int AutoMult;
    public int desiredPosition;
    public int RAMSpeed;
    public float camSpeed;
    public Text displayPts;
    public Text displayMoney;
    public Camera cam;

    public Transform upgradeContainer;
    public Transform projectContainer;
    public List<GameObject> upgradeList;
    public List<GameObject> upgradesInScene;

    public Text displayClickAmount;
    public Text displayAutoClick;

    // Start is called before the first frame update
    void Start()
    {
        ClickAmount = 1;
        ClickMult = 1;
        AutoClick = 0;
        AutoMult = 1;
        RAMSpeed = 1;
        //pts = 10000; //debug
        //money = 10000; //debug
        foreach(GameObject up in upgradeList)
        {
            if (!up.GetComponent<Upgrade>().isProject)
            {
                upgradesInScene.Add(Instantiate(up, upgradeContainer));
            }
            else
            {
                upgradesInScene.Add(Instantiate(up, projectContainer));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        pts += AutoClick * AutoMult * Time.deltaTime;
        displayPts.text = pts.ToString("F0");
        displayMoney.text = "$" + money.ToString("F2");
        displayClickAmount.text = "Click: " + ClickAmount.ToString();
        if (ClickMult > 1)
        {
            displayClickAmount.text += " x " + ClickMult.ToString() + " = " + (ClickAmount * ClickMult).ToString();
        }
        displayAutoClick.text = "Auto: "+ AutoClick.ToString();
        if (AutoMult > 1)
        {
            displayAutoClick.text += " x " + AutoMult.ToString() + " = " + (AutoClick * AutoMult).ToString();
        }
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), Time.deltaTime * camSpeed);

    }

    public void Click()
    {
        pts += ClickAmount * ClickMult;
        displayClickAmount.gameObject.GetComponent<AudioSource>().Play();
    }

    public void MoveCam(string dir)
    {
        if (dir == "l")
        {
            desiredPosition = -230;
        }
        else if (dir == "m")
        {
            desiredPosition = 0;
        }
        else if (dir == "r")
        {
            desiredPosition = 230;
        }
        else if (dir == "ll")
        {
            desiredPosition = -460;
        }
        else if (dir == "rr")
        {
            desiredPosition = 460;
        }
    }

}
