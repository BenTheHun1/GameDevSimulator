using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolveManager : MonoBehaviour
{
    GameManager gm;
    public Button evolveButton;
    public Text displayEvolveCost;
    int[] nextEraCost;

    public Text buttonText;
    public ParticleSystem clickparticles;
    public Image BG;

    public Material Era1Particle;
    public Material Era2Particle;

    public Sprite Era1BG;
    public Sprite Era2BG;


    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();

        nextEraCost = new int[2];
        nextEraCost[0] = 5; nextEraCost[1] = 2;
        
        evolveButton.onClick.AddListener(NextEra);
    }

    public void NextEra()
    {
        if (gm.nm.disNum >= nextEraCost[0] && gm.nm.disNumAbb >= nextEraCost[1])
        {
            gm.era++;
            ReloadEra();
        }
    }


    public void ReloadEra()
    {
        if (gm.era == 1)
        {
            nextEraCost[1] = 2;
            displayEvolveCost.text = "To Next Era: " + gm.nm.FormatForDisplay(nextEraCost[0], nextEraCost[1]);
            buttonText.text = "Code!";
            gm.nm.resourceType = "Code";
            gm.moneyType = "Dollars";
            clickparticles.gameObject.GetComponent<ParticleSystemRenderer>().material = Era1Particle;
            BG.sprite = Era1BG;
        }
        else if (gm.era == 2)
        {
            nextEraCost[1] = 3;
            displayEvolveCost.text = "To Next Era: " + gm.nm.FormatForDisplay(nextEraCost[0], nextEraCost[1]);
            buttonText.text = "Fight!";
            gm.nm.resourceType = "EXP";
            gm.moneyType = "Gold";
            clickparticles.gameObject.GetComponent<ParticleSystemRenderer>().material = Era2Particle;
            BG.sprite = Era2BG;
        }
    }
}
