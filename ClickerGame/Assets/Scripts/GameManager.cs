using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
public enum quantity
{
    Repeatable,
    Single,
    Static
}
public class GameManager : MonoBehaviour
{
    public float money;
    public int ClickAmount;
    public int ClickMult;
    public float AutoClick;
    public float AutoMult;
    public float desiredPosition;
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

    public double pts;
    public string resource;
    public int era;
    public EvolveManager em;
    public string moneyType;
    public ParticleSystem clickparticles;

    public float prestige;

	public List<GameObject> windows;
	public Canvas worldCanvas;

    // Start is called before the first frame update
    void Start()
    {
		foreach (GameObject window in windows)
		{
			window.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 2, 1000);
		}
		worldCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 12, 1600);


		//Application.targetFrameRate = Device
		em = gameObject.GetComponent<EvolveManager>();
        era = 1;
        ClickAmount = 1;
        ClickMult = 1;
        AutoClick = 0;
        AutoMult = 1;
        RAMSpeed = 1;
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
		//InvokeRepeating("Auto", 1.0f, 1.0f);
		//MoveCam("m");
	}

    void Auto()
    {
        //pts += AutoClick * AutoMult * ((100 + prestige * 10) / 100);
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(windows[2].transform.position.x);

		pts += (AutoClick * AutoMult * ((100 + prestige) / 100)) * Time.deltaTime;

        displayPts.text = pts.ToString("F0") + " " + resource;
        displayMoney.text = money.ToString("F2") + " " + moneyType;
        displayClickAmount.text = resource + "/Click: " + ClickAmount.ToString();
        if (ClickMult > 1 && ClickAmount > 0)
        {
            displayClickAmount.text += " x " + ClickMult.ToString() + " = " + (ClickAmount * ClickMult).ToString();
        }
        displayAutoClick.text = resource + "/Sec: " + AutoClick.ToString();
        if (AutoMult > 1 && AutoClick > 0)
        {
            displayAutoClick.text += " x " + AutoMult.ToString() + " = " + (AutoClick * AutoMult).ToString();
        }
        if (prestige > 0 && AutoClick > 0)
        {
            displayAutoClick.text += " x " + ((100 + prestige) / 100).ToString("F2");
        }
        //cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), Time.deltaTime * camSpeed);

        if (Input.GetKeyDown(KeyCode.P))
        {
            pts += 100000;
        }


        //Debug.Log(Screen.width);
    }

    public void Click()
    {
        pts += ClickAmount * ClickMult;
        displayClickAmount.gameObject.GetComponent<AudioSource>().Play();
        clickparticles.Play();
    }

    public void MoveCam(string dir)
    {
		if (dir == "ll")
		{
			//desiredPosition = -470;

			desiredPosition = windows[0].transform.position.x;
		}
		else if (dir == "l")
        {
			//desiredPosition = -235;
			desiredPosition = windows[1].transform.position.x;
		}
        else if (dir == "m")
        {
            //desiredPosition = 0;
			desiredPosition = windows[2].transform.position.x;
		}
        else if (dir == "r")
        {
			//desiredPosition = 235;

			desiredPosition = windows[3].transform.position.x;
		}
        else if (dir == "rr")
        {
			//desiredPosition = 470;

			desiredPosition = windows[4].transform.position.x;
		}

		LeanTween.move(cam.gameObject, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), 0.1f);
		Debug.Log("Moving to " + desiredPosition + " at " + dir);
	}

	public void StartPos()
	{
		desiredPosition = windows[2].transform.position.x;
		LeanTween.move(cam.gameObject, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), 0f);
	}
	
}
