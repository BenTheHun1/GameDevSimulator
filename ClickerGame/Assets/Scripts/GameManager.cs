using System.Collections;
using System.Collections.Generic;
using TMPro;
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
	public bool debugMode;
	public float money;
    public int ClickAmount;
    public int ClickMult;
    public float AutoClick;
    public float AutoMult;
    public float desiredPosition;
    public int RAMSpeed;
    public float camSpeed;
    public TextMeshProUGUI displayPts;
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
	public TextMeshProUGUI versionDisplay;
	public float curPosition;

	public GameObject quitGame;

	public AudioClip clickSound;


    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(Screen.currentResolution.width);
		foreach (GameObject window in windows)
		{
			window.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.width / 1.5f, 1000);
		}
		worldCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.width * 4, 1600);

		versionDisplay.text = Application.version;
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			Application.targetFrameRate = 60;
		}

		if (SystemInfo.deviceType == DeviceType.Desktop && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor))
		{
			quitGame.gameObject.SetActive(true);
		}
		else
		{
			quitGame.gameObject.SetActive(false);
		}
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

        if (Input.GetKeyDown(KeyCode.P) && debugMode)
        {
            pts += 100000;
        }

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			MoveCam(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			MoveCam(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			MoveCam(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			MoveCam(3);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			MoveCam(4);
		}

		//Debug.Log(Screen.width);
	}

    public void Click()
    {
        pts += ClickAmount * ClickMult;
		GetComponent<AudioSource>().PlayOneShot(clickSound);
        clickparticles.Play();
    }

    public void MoveCam(int pos)
    {
		desiredPosition = windows[pos].transform.position.x;
		float speed = 0.0005f * Mathf.Abs(windows[pos].transform.position.x - cam.transform.position.x);
		LeanTween.cancelAll();
		LeanTween.move(cam.gameObject, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), speed);
		curPosition = pos;
		Debug.Log("Moving to " + desiredPosition + " at position " + pos + " with speed " + speed);
	}

	public void StartPos()
	{
		desiredPosition = windows[2].transform.position.x;
		LeanTween.move(cam.gameObject, new Vector3(desiredPosition, cam.transform.position.y, cam.transform.position.z), 0f);
		curPosition = 2;
	}
	
}
