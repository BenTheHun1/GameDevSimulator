using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EvolveManager : MonoBehaviour
{
    GameManager gm;
    public Button evolveButton;
    public Button resetButton;
    public GameObject resetOptions;
    public Text displayEvolveCost;
    double nextEraCost;

    public Text buttonText;
    public Text era;
    public Text lore;
    public ParticleSystem clickparticles;
    public Image BG;

    public Material Era1Particle;
    public Material Era2Particle;
    public Material Era3Particle;

    public Sprite Era1BG;
    public Sprite Era2BG;
    public Sprite Era3BG;

    public Text disPrestige;

    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
        nextEraCost = 5000f;
        
        evolveButton.onClick.AddListener(NextEra);
        resetButton.onClick.AddListener(Reset);
        resetOptions.SetActive(false);
    }

    void Update()
    {
        disPrestige.text = "Current: " + gm.prestige.ToString("F2") + " Reset for: " + (gm.pts / 1000).ToString("F2");
    }

    public void NextEra()
    {
        if (gm.pts >= nextEraCost)
        {
            gm.era++;
            ReloadEra();
        }
    }

    public void Reset()
    {
        gm.prestige += (float)gm.pts / 1000;
        gm.pts = 0;
        gm.ClickAmount = 1;
        gm.ClickMult = 1;
        gm.AutoClick = 0;
        gm.AutoMult = 1;
        gm.RAMSpeed = 1;
        gm.money = 0;
        gm.era = 1;
        foreach (GameObject upgrade in gm.upgradesInScene)
        {
            upgrade.GetComponent<Upgrade>().timesPurchased = 0;
        }
        gameObject.GetComponent<SaveScript>().SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ReloadEra()
    {
        if (gm.era == 1)
        {
            era.text = "Clicker Era";
            displayEvolveCost.text = "To Next Era: " + nextEraCost;
            buttonText.text = "Code!";
            gm.resource = "Code";
            gm.moneyType = "Dollars";
            clickparticles.gameObject.GetComponent<ParticleSystemRenderer>().material = Era1Particle;
            BG.sprite = Era1BG;
            lore.text = "You, an amateur programmer, start developing a video game. What do the kids like these days? Watching numbers go up? A Clicker game is the way to go.";
            foreach (GameObject ug in gm.upgradesInScene)
            {
                if (ug.GetComponent<Upgrade>().eraRequired == gm.era)
                {
                    ug.SetActive(true);
                }
                else
                {
                    ug.SetActive(false);
                }
            }
        }
        else if (gm.era == 2)
        {
            era.text = "JRPG Era";
            gm.pts -= nextEraCost;
            nextEraCost *= 10;
            displayEvolveCost.text = "To Next Era: " + nextEraCost;
            buttonText.text = "Fight!";
            gm.resource = "EXP";
            gm.moneyType = "Gold";
            clickparticles.gameObject.GetComponent<ParticleSystemRenderer>().material = Era2Particle;
            BG.sprite = Era2BG;
            lore.text = "You change your mind. Roleplaying games are all the rage. Japanese, Western, doesn't matter. You'll make a retro throwback RPG, no one ahs ever done that before. You retool your game to be an RPG.";
            foreach (GameObject ug in gm.upgradesInScene)
            {
                if (ug.GetComponent<Upgrade>().eraRequired == gm.era)
                {
                    ug.SetActive(true);
                }
                else
                {
                    ug.SetActive(false);
                }
            }
        }
        else if (gm.era == 3)
        {
            era.text = "FPS Era";
            evolveButton.enabled = false;
            resetOptions.SetActive(true);
            displayEvolveCost.text = "Max Era";
            buttonText.text = "Shoot!";
            gm.resource = "Kills";
            gm.moneyType = "Keys";
            clickparticles.gameObject.GetComponent<ParticleSystemRenderer>().material = Era3Particle;
            BG.sprite = Era3BG;
            lore.text = "No no no, what you really need is something Esports worthy. A first person shooter, emphasizing tactics and skill over randomness. You'll tread new ground, and make a game every will flock to. You retool your game to be an FPS.";
            foreach (GameObject ug in gm.upgradesInScene)
            {
                if (ug.GetComponent<Upgrade>().eraRequired == gm.era)
                {
                    ug.SetActive(true);
                }
                else
                {
                    ug.SetActive(false);
                }
            }
        }
    }
}
